using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO.UserDTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	// DONE
	[Route("/[controller]")]
	[ApiController]
	public class UsersController(IUserService<User> service, IMapper mapper, SharedService sharedService, API_ResponseConvention api_Response) : ControllerBase
	{
		private readonly IUserService<User> _service = service;
		private readonly IMapper _mapper = mapper;
		private readonly SharedService _sharedService = sharedService;
		private readonly API_ResponseConvention _api_Response = api_Response;

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetUsers()
		{
			string methodName = nameof(GetUsers);

			ICollection<User> _items = await _service.GetItems();

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
		public async Task<IActionResult> GetUser(Guid id)
		{
			string methodName = nameof(GetUser);

			User _item = await _service.GetItem(id);

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
		public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO item)
		{
			string methodName = nameof(CreateUser);

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
		public async Task<IActionResult> PatchUser(Guid id, [FromBody] UserCreateDTO item)
		{
			string methodName = nameof(PatchUser);

			User _item = await _service.GetItem(id);

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
			bool isUpdated = await _service.UpdateItem(_item, patchItem);
			if (isUpdated)
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
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			string methodName = nameof(DeleteUser);

			User _item = await _service.GetItem(id);

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
