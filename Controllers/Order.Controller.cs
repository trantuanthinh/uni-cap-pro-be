using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using uni_cap_pro_be.Interfaces;
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
		public IActionResult GetProducts()
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			ICollection<Order> _items = _service.GetItems();

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _items);
			return StatusCode(200, okMessage);
		}

		// Other action methods like GetProduct, CreateProduct, PatchProduct, and DeleteProduct would follow a similar structure.
	}
}
