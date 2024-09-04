using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO.OrderDTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	// TODO
	[Route("/[controller]")]
	[ApiController]
	public class OrdersController(IOrderService<Order> service,
		ISub_OrderService<Sub_Order> subOrderService,
		IMapper mapper, API_ResponseConvention api_Response) : ControllerBase
	{
		private readonly IOrderService<Order> _service = service;
		private readonly ISub_OrderService<Sub_Order> _subOrderService = subOrderService;
		private readonly IMapper _mapper = mapper;
		private readonly API_ResponseConvention _api_Response = api_Response;

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetOrders()
		{
			string methodName = nameof(GetOrders);

			ICollection<Order> _items = await _service.GetItems();

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			var _dtos = new List<OrderDTO>();
			foreach (var item in _items)
			{
				List<Sub_Order> sub_orders = await _subOrderService.GetSubOrdersById(item.Id);
				item.Sub_Orders = sub_orders;

				OrderDTO _item = _mapper.Map<OrderDTO>(item);
				_dtos.Add(_item);
			}

			var okMessage = _api_Response.OkMessage(methodName, _dtos);
			return StatusCode(200, okMessage);
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetOrder(Guid id)
		{
			string methodName = nameof(GetOrder);

			Order _item = await _service.GetItem(id);

			if (_item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			List<Sub_Order> sub_orders = await _subOrderService.GetSubOrdersById(_item.Id);
			_item.Sub_Orders = sub_orders;
			OrderDTO _dto = _mapper.Map<OrderDTO>(_item);

			var okMessage = _api_Response.OkMessage(methodName, _dto);
			return StatusCode(200, okMessage);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateOrder([FromBody] Sub_OrderCreateDTO sub_orderDto)
		{
			string methodName = nameof(CreateOrder);

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			Order _order = new Order
			{
				Total_Price = 0,
				Total_Quantity = 0
			};
			Sub_Order _suborder = _mapper.Map<Sub_Order>(sub_orderDto);

			_order.Level = 1;
			_order.Total_Price += _suborder.Price;
			_order.Total_Quantity += _suborder.Quantity;

			bool isOrderCreated = await _service.CreateItem(_order);
			if (!isOrderCreated)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			_suborder.OrderId = _order.Id;
			bool isSubOrderCreated = await _subOrderService.CreateItem(_suborder);
			if (!isSubOrderCreated)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _order);
			return StatusCode(200, okMessage);
		}

		[Authorize]
		[HttpPatch("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PatchOrder(Guid id, [FromBody] Sub_OrderCreateDTO sub_orderDto)
		{
			//string methodName = nameof(PatchOrder);

			//Order _item = await _service.GetItem(id);

			//if (item == null || _item == null)
			//{
			//	var failedMessage = _api_Response.FailedMessage(methodName);
			//	return StatusCode(404, failedMessage);
			//}

			//if (!TryValidateModel(_item))
			//{
			//	return ValidationProblem(ModelState);
			//}

			//Order patchItem = _mapper.Map<Order>(item);
			//bool isUpdated = await _service.UpdateItem(_item, patchItem);
			//if (isUpdated)
			//{
			//	var failedMessage = _api_Response.FailedMessage(methodName);
			//	return StatusCode(500, failedMessage);
			//}

			//var okMessage = _api_Response.OkMessage(methodName, _item);
			//return StatusCode(200, okMessage);
			return Ok(200);
		}

		[HttpDelete("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteOrder(Guid id)
		{
			string methodName = nameof(DeleteOrder);

			Order _item = await _service.GetItem(id);

			if (_item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			bool isDeleted = await _service.DeleteItem(_item);
			if (!isDeleted)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}
	}
}
