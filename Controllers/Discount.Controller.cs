using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO.DiscountDTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	// DONE
	[Route("/[controller]")]
	[ApiController]
	public class DiscountsController(
		IDiscountService<Discount> service,
		IMapper mapper,
		API_ResponseConvention api_Response
	) : ControllerBase
	{
		private readonly IDiscountService<Discount> _service = service;
		private readonly IMapper _mapper = mapper;
		private readonly API_ResponseConvention _api_Response = api_Response;

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetDiscounts()
		{
			string methodName = nameof(GetDiscounts);

			ICollection<Discount> _items = await _service.GetItems();

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
		public async Task<IActionResult> GetDiscount(Guid id)
		{
			string methodName = nameof(GetDiscount);

			Discount _item = await _service.GetItem(id);

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
		public async Task<IActionResult> CreateDiscount([FromBody] DiscountCreateDTO item)
		{
			string methodName = nameof(CreateDiscount);

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			Discount _item = _mapper.Map<Discount>(item);
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
		public async Task<IActionResult> PatchDiscount(Guid id, [FromBody] DiscountCreateDTO item)
		{
			string methodName = nameof(PatchDiscount);

			Discount _item = await _service.GetItem(id);

			if (item == null || _item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			Discount patchItem = _mapper.Map<Discount>(item);
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
		public async Task<IActionResult> DeleteDiscount(Guid id)
		{
			string methodName = nameof(DeleteDiscount);

			Discount _item = await _service.GetItem(id);

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
