// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using uni_cap_pro_be.Core;
// using uni_cap_pro_be.DTO.Request;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Services;

// namespace uni_cap_pro_be.Controllers
// {
// 	// TODO
// 	[Route("/[controller]")]
// 	[ApiController]
// 	public class OrdersController : BaseAPIController
// 	{
// 		private readonly OrderService _service;
// 		//private readonly IProductService _productService;
// 		//private readonly ISub_OrderService _subOrderService;

// 		public OrdersController(
// 			OrderService service
// 		//IProductService productService,
// 		//ISub_OrderService subOrderService
// 		)
// 			: base()
// 		{
// 			_service = service;
// 			//_productService = productService;
// 			//_subOrderService = subOrderService;
// 		}

// 		// [HttpGet]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
// 		// public async Task<IActionResult> GetOrders([FromQuery] QueryParameters queryParameters)
// 		// {
// 		// 	string methodName = nameof(GetOrders);

// 		// 	ICollection<Order> _items = await _service.GetOrders(queryParameters);

// 		// 	// IQueryable<Order> query = _dataContext.Orders.AsQueryable();
// 		// 	// query = _sharedService.GetFilterQuery(query, customFilter);
// 		// 	// if (!string.IsNullOrEmpty(search))
// 		// 	//     query = query.Where(p => p..Contains(search));

// 		// 	// var result = query.ToList();
// 		// 	// var _dtos = new List<OrderDTO>();
// 		// 	// foreach (var item in _items)
// 		// 	// {
// 		// 	//     // List<Sub_Order> sub_orders = await _subOrderService.GetSubOrdersById(item.Id);
// 		// 	//     // item.Sub_Orders = sub_orders;

// 		// 	//     Product product = await _productService.GetProduct(item.ProductId);
// 		// 	//     item.Product = product;

// 		// 	//     OrderDTO _item = _mapper.Map<OrderDTO>(item);
// 		// 	//     _item.TimeLeft = _item.EndTime - DateTime.UtcNow;
// 		// 	//     _item.Is_Remained = _item.TimeLeft > TimeSpan.Zero;

// 		// 	//     _dtos.Add(_item);
// 		// 	// }

// 		// 	// var okMessage = _apiResponse.Success(methodName, _dtos);
// 		// 	var okMessage = _response.OkMessage(methodName, _items);
// 		// 	// return StatusCode(200, okMessage);
// 		// 	return Ok();
// 		// }

// 		// [HttpGet("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status401Unauthorized)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// //public async Task<IActionResult> GetOrder(Guid id)
// 		// //{
// 		// //    string methodName = nameof(GetOrder);

// 		// //    Order _item = await _service.GetOrder(id);

// 		// //    if (_item == null)
// 		// //    {
// 		// //        var failedMessage = _apiResponse.Failure(methodName);
// 		// //        return StatusCode(404, failedMessage);
// 		// //    }

// 		// //    List<Sub_Order> sub_orders = await _subOrderService.GetSubOrdersById(_item.Id);
// 		// //    _item.Sub_Orders = sub_orders;

// 		// //    Product product = await _productService.GetProduct(_item.ProductId);
// 		// //    _item.Product = product;

// 		// //    OrderDTO _dto = _mapper.Map<OrderDTO>(_item);
// 		// //    _dto.TimeLeft = _dto.EndTime - DateTime.UtcNow;
// 		// //    _dto.Is_Remained = _dto.TimeLeft > TimeSpan.Zero;

// 		// //    var okMessage = _apiResponse.Success(methodName, _dto);
// 		// //    return StatusCode(200, okMessage);
// 		// //}

// 		// [HttpPost]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
// 		// [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// //public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderDto)
// 		// //{
// 		// //    string methodName = nameof(CreateOrder);

// 		// //    if (!ModelState.IsValid)
// 		// //    {
// 		// //        var failedMessage = _apiResponse.Failure(methodName, ModelState);
// 		// //        return StatusCode(400, failedMessage);
// 		// //    }

// 		// //    Sub_Order _suborder = _mapper.Map<Sub_Order>(orderDto);

// 		// //    Order _order = new Order
// 		// //    {
// 		// //        ProductId = orderDto.ProductId,
// 		// //        Total_Price = orderDto.Price,
// 		// //        Total_Quantity = orderDto.Quantity,
// 		// //        IsShare = orderDto.IsShare,
// 		// //        IsPaid = false,
// 		// //        Level = 1,
// 		// //    };

// 		// //    bool isOrderCreated = await _service.CreateOrder(_order);
// 		// //    if (!isOrderCreated)
// 		// //    {
// 		// //        var failedMessage = _apiResponse.Failure(methodName);
// 		// //        return StatusCode(500, failedMessage);
// 		// //    }

// 		// //    _suborder.OrderId = _order.Id;
// 		// //    bool isSubOrderCreated = await _subOrderService.CreateSub_Order(_suborder);
// 		// //    if (!isSubOrderCreated)
// 		// //    {
// 		// //        var failedMessage = _apiResponse.Failure(methodName);
// 		// //        return StatusCode(500, failedMessage);
// 		// //    }

// 		// //    var okMessage = _apiResponse.Success(methodName, _order);
// 		// //    return StatusCode(200, okMessage);
// 		// //}

// 		// [Authorize]
// 		// [HttpPatch("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> PatchOrder(Guid id, [FromBody] OrderCreateDTO orderDto)
// 		// {
// 		// 	//string methodName = nameof(PatchOrder);

// 		// 	//Order _item = await _service.GetItem(id);

// 		// 	//if (item == null || _item == null)
// 		// 	//{
// 		// 	//	var failedMessage = _apiResponse.Failure(methodName);
// 		// 	//	return StatusCode(404, failedMessage);
// 		// 	//}

// 		// 	//if (!TryValidateModel(_item))
// 		// 	//{
// 		// 	//	return ValidationProblem(ModelState);
// 		// 	//}

// 		// 	//Order patchItem = _mapper.Map<Order>(item);
// 		// 	//bool isUpdated = await _service.UpdateItem(_item, patchItem);
// 		// 	//if (isUpdated)
// 		// 	//{
// 		// 	//	var failedMessage = _apiResponse.Failure(methodName);
// 		// 	//	return StatusCode(500, failedMessage);
// 		// 	//}

// 		// 	//var okMessage = _apiResponse.Success(methodName, _item);
// 		// 	//return StatusCode(200, okMessage);
// 		// 	return Ok(200);
// 		// }

// 		// [HttpDelete("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> DeleteOrder(Guid id)
// 		// {
// 		// 	string methodName = nameof(DeleteOrder);

// 		// 	Order _item = await _service.GetOrder(id);

// 		// 	if (_item == null)
// 		// 	{
// 		// 		var failedMessage = _response.FailedMessage(methodName);
// 		// 		return StatusCode(404, failedMessage);
// 		// 	}

// 		// 	bool isDeleted = await _service.DeleteOrder(_item);
// 		// 	if (!isDeleted)
// 		// 	{
// 		// 		var failedMessage = _response.FailedMessage(methodName);
// 		// 		return StatusCode(500, failedMessage);
// 		// 	}

// 		// 	var okMessage = _response.OkMessage(methodName, _item);
// 		// 	return StatusCode(200, okMessage);
// 		// }
// 	}
// }
