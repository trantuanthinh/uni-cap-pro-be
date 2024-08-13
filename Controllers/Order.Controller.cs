using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class OrderController(IBaseService<Order> service, IMapper mapper, API_ResponseConvention api_Response) : ControllerBase
	{
		private readonly IBaseService<Order> _service = service;
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

			var okMessage = _api_Response.OkMessage(methodName, _items);
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

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateOrder([FromBody] OrderDTO item)
		{
			string methodName = nameof(CreateOrder);

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			Order _item = _mapper.Map<Order>(item);
			bool isCreated = await _service.CreateItem(_item);
			if (!isCreated)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}

		[Authorize]
		[HttpPatch("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PatchOrder(Guid id, [FromBody] ProductDTO item) // TODO: change to OrderDTO
		{
			string methodName = nameof(PatchOrder);

			Order _item = await _service.GetItem(id);

			if (item == null || _item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			Order patchItem = _mapper.Map<Order>(item);
			bool isUpdated = await _service.UpdateItem(_item, patchItem);
			if (isUpdated)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
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
