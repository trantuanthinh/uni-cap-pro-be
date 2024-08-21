using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	// TODO
	[Route("/[controller]")]
	[ApiController]
	public class Sub_OrdersController(ISub_OrderService<Sub_Order> service, IMapper mapper, SharedService sharedService, API_ResponseConvention api_Response) : ControllerBase

	{
		private readonly ISub_OrderService<Sub_Order> _service = service;
		private readonly IMapper _mapper = mapper;
		private readonly SharedService _sharedService = sharedService;
		private readonly API_ResponseConvention _api_Response = api_Response;

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetSub_Orders()
		{
			string methodName = nameof(GetSub_Orders);

			ICollection<Sub_Order> _items = await _service.GetItems();

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
		public async Task<IActionResult> GetSub_Order(Guid id)
		{
			string methodName = nameof(GetSub_Order);

			Sub_Order _item = await _service.GetItem(id);

			if (_item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}
	}
}