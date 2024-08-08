using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/")]
	[ApiController]
	public class UserController(IUserService userService, IMapper mapper, SharedService sharedService) : Controller
	{
		private readonly IUserService _userService = userService;
		private readonly IMapper _mapper = mapper;
		private readonly SharedService _sharedService = sharedService;

		[HttpGet("users")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetUsers()
		{
			ICollection<User> _items = _userService.GetUsers();

			if (!ModelState.IsValid)
			{
				return StatusCode(400, ModelState);
			}
			return StatusCode(200, new { data = _items });
		}

		[HttpGet("user/{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetUser(Guid id)
		{

			User _item = _userService.GetUser(id);

			if (_item == null)
			{
				return StatusCode(404, new { message = "User not found." });
			}

			return StatusCode(200, new { data = _item });
		}

		[HttpPost("user")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CreateUser([FromBody] UserDTO item)
		{
			if (!_sharedService.IsValidGmail(item.Email))
			{
				ModelState.AddModelError("", "Invalid email address");
				return StatusCode(400, ModelState);
			}

			if (!ModelState.IsValid)
			{
				return StatusCode(400, ModelState);
			}

			User _item = _mapper.Map<User>(item);
			bool isCreated = _userService.CreateUser(_item);
			if (!isCreated)
			{
				ModelState.AddModelError("", "Invalid. Something went wrong creating User.");
				return StatusCode(500, ModelState);
			}
			return StatusCode(200, new { message = "Created Successfully", data = _item });
		}

		[Authorize]
		[HttpPatch("user/{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult PatchUser(Guid id, [FromBody] UserDTO item)
		{
			User _item = _userService.GetUser(id);

			if (item == null || _item == null)
			{
				return StatusCode(404, ModelState);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			User patchItem = _mapper.Map<User>(item);
			if (!_userService.UpdateUser(_item, patchItem))
			{
				ModelState.AddModelError("", "Invalid - Something went wrong updating the User");
				return StatusCode(500, ModelState);
			}

			return StatusCode(200, new { message = "Updated Successfully", data = _item });
		}

		[HttpDelete("user/{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteUser(Guid id)
		{
			User _item = _userService.GetUser(id);

			if (_item == null)
			{
				return StatusCode(404, new { message = "User not found" });
			}

			bool isDeleted = _userService.DeleteUser(_item);

			if (!isDeleted)
			{
				return StatusCode(500, new { message = "An error occurred while deleting the User" });
			}

			return StatusCode(202, new { message = "Deleted Successfully" });
		}
	}
}
