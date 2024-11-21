using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // TODO
    [Route("/[controller]")]
    [ApiController]
    public class OrdersController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        OrderService service,
        Sub_OrderService subOrderService,
        Item_OrderService itemOrderService
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly OrderService _service = service;
        private readonly Sub_OrderService _subOrderService = subOrderService;
        private readonly Item_OrderService _itemOrderService = itemOrderService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders([FromQuery] QueryParameters queryParameters)
        {
            string methodName = nameof(GetOrders);

            BaseResponse<OrderResponse> _items = await _service.GetOrders(queryParameters);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            string methodName = nameof(GetOrder);

            OrderResponse _item = await _service.GetOrder(id);

            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest item)
        {
            string methodName = nameof(CreateOrder);

            Order _order = new Order
            {
                Id = Guid.NewGuid(),
                Total_Price = item.Total_Price,
                DistrictId = item.DistrictId,
                IsShare = item.IsShare,
                Delivery_Status = DeliveryStatus.PENDING,
                IsActive = true
            };
            bool isOrderCreated = await _service.CreateOrder(_order);
            if (!isOrderCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            Sub_Order _sub_Order = new Sub_Order
            {
                Id = Guid.NewGuid(),
                OrderId = _order.Id,
                UserId = item.UserId,
                IsPaid = false
            };
            bool isSubOrderCreated = await _subOrderService.CreateSub_Order(_sub_Order);
            if (!isSubOrderCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            foreach (ItemRequest itemRequest in item.ItemRequests)
            {
                Item_Order _itemOrder = new Item_Order
                {
                    Id = Guid.NewGuid(),
                    Sub_OrderId = _sub_Order.Id,
                    ProductId = itemRequest.ProductId,
                    Quantity = itemRequest.Quantity,
                    IsRating = false
                };
                bool isItemOrderCreated = await _itemOrderService.CreateItem_Order(_itemOrder);
                if (!isItemOrderCreated)
                {
                    var failedMessage = _apiResponse.Failure(methodName);
                    return StatusCode(500, failedMessage);
                }
            }

            var okMessage = _apiResponse.Success(methodName, _order);
            return StatusCode(200, okMessage);
            // return Ok();
        }

        [HttpPost("group-buy/{orderId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddSubOrder(
            Guid orderId,
            [FromBody] BuyTogetherRequest item
        )
        {
            string methodName = nameof(AddSubOrder);

            Sub_Order _suborder = _mapper.Map<Sub_Order>(item);

            Order _order = await _service.FindOrder(orderId);
            if (_order == null)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }

            bool checkValid = _service.CheckValid(_order);
            if (!checkValid)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(412, failedMessage);
            }

            _suborder.OrderId = orderId;
            bool isSubOrderCreated = await _subOrderService.CreateSub_Order(_suborder);
            if (!isSubOrderCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            bool isSubOrderAdded = await _service.AddSubOrder(_order);
            if (!isSubOrderAdded)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, _order);
            return StatusCode(200, okMessage);
        }

        // [Authorize]
        [HttpPatch("{orderId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchOrder(
            Guid orderId,
            [FromBody] PatchRequest<OrderRequest> patchRequest
        )
        {
            string methodName = nameof(PatchOrder);

            bool isUpdated = await _service.UpdateOrder(orderId, patchRequest);
            if (!isUpdated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }
            var okMessage = _apiResponse.Success(methodName, patchRequest);
            return StatusCode(200, okMessage);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            string methodName = nameof(DeleteOrder);

            bool isDeleted = await _service.DeleteOrder(id);
            if (!isDeleted)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, id);
            return StatusCode(200, okMessage);
        }
    }
}
