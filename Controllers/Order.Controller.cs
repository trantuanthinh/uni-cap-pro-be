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
        Sub_OrderService subOrderService
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly OrderService _service = service;
        private readonly Sub_OrderService _subOrderService = subOrderService;

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
            // string methodName = nameof(CreateOrder);

            // Sub_Order _suborder = _mapper.Map<Sub_Order>(item);

            // Order _order = new Order
            // {
            //     Id = Guid.NewGuid(),
            //     Total_Price = item.Price,
            //     IsShare = item.IsShare,
            //     IsPaid = false
            // };

            // bool isOrderCreated = await _service.CreateOrder(_order);
            // if (!isOrderCreated)
            // {
            //     var failedMessage = _apiResponse.Failure(methodName);
            //     return StatusCode(500, failedMessage);
            // }

            // _suborder.OrderId = _order.Id;
            // bool isSubOrderCreated = await _subOrderService.CreateSub_Order(_suborder);
            // if (!isSubOrderCreated)
            // {
            //     var failedMessage = _apiResponse.Failure(methodName);
            //     return StatusCode(500, failedMessage);
            // }

            // var okMessage = _apiResponse.Success(methodName, _order);
            // return StatusCode(200, okMessage);
            return Ok();
        }

        [HttpPost("buy-together/{orderId:guid}")]
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
