using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/api/[controlLer]")]
	[ApiController]
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly SharedService _sharedService;

		public UserController(IUserRepository userRepository, IUserService userService, IMapper mapper, SharedService sharedService)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_userService = userService;
			_sharedService = sharedService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetUsers()
		{
			var _users = _userRepository.GetUsers();

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			return Ok(new { data = _users });
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<User> GetUser(Guid id)
		{
			var _user = _userRepository.GetUser(id);

			if (_user == null)
			{
				return NotFound(new { message = "User not found." });
			}

			return Ok(new { data = _user });
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		public ActionResult<User> CreateUser([FromBody] UserDTO user)
		{

			var isUser = _userService.GetUserTrimToUpper(user);

			if (isUser != null)
			{
				ModelState.AddModelError("", "User already exists");
				return UnprocessableEntity(ModelState);
			}

			if (!_sharedService.IsValidGmail(user.Email))
			{
				ModelState.AddModelError("", "Invalid email address");
				return BadRequest(ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			user.Password = _userService.HashPassword(user.Password);

			var _user = _mapper.Map<User>(user);
			_user.Created_At = DateTime.UtcNow;
			_user.Modified_At = DateTime.UtcNow;

			if (!_userRepository.CreateUser(_user))
			{
				ModelState.AddModelError("", "Something went wrong creating user");
				return StatusCode(500, ModelState);
			}
			return Ok(new { message = "Successfully", data = _user });
		}
	}
}
