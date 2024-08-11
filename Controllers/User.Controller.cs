using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class UsersController(
		IUserService userService,
		IMapper mapper,
		SharedService sharedService,
		API_ResponseConvention api_Response
	) : ControllerBase
	{
		private readonly IUserService _userService = userService;
		private readonly IMapper _mapper = mapper;
		private readonly SharedService _sharedService = sharedService;
		private readonly API_ResponseConvention _api_Response = api_Response;

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetUsers()
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			ICollection<User> _items = _userService.GetUsers();

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
		public IActionResult GetUser(Guid id)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			User _item = _userService.GetUser(id);

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
		public IActionResult CreateUser([FromBody] UserDTO item)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			if (!_sharedService.IsValidGmail(item.Email))
			{
				ModelState.AddModelError("", "Invalid email address");
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			User _item = _mapper.Map<User>(item);

			bool isCreated = _userService.CreateUser(_item);
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
		public IActionResult PatchUser(Guid id, [FromBody] UserDTO item)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			User _item = _userService.GetUser(id);

			if (item == null || _item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(404, failedMessage);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			User patchItem = _mapper.Map<User>(item);
			if (!_userService.UpdateUser(_item, patchItem))
			{
				ModelState.AddModelError("", "Invalid - Something went wrong updating the User");
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage($"{methodName} Successfully", _item);
			return StatusCode(200, okMessage);
		}

		[Authorize]
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteUser(Guid id)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			User _item = _userService.GetUser(id);

			if (_item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			bool isDeleted = _userService.DeleteUser(_item);
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
