using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController(IAuthService authService, JWTService jwtService) : Controller
	{
		private readonly IAuthService _authService = authService;
		private readonly JWTService _jwtService = jwtService;

		[HttpPost("signin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public ActionResult<User> Signin([FromBody] SignInDTO item)
		{
			User _user = _authService.AuthenticatedUser(item);

			Console.WriteLine(_user);
			if (_user == null)
			{
				ModelState.AddModelError("", "Invalid email/ phone/ username or password");
				return StatusCode(401, ModelState);
			}

			string token = _jwtService.GenerateJwtToken(_user);

			return StatusCode(200, new { Token = token, User = _user });
		}


		//[HttpPost("signup")]
		//public ActionResult<User> Signup([FromBody] UserDTO userDto)
		//{
		//	// Check if the user already exists
		//	if (!_sharedService.CheckValidUser(userDto))
		//	{
		//		ModelState.AddModelError("", "User already exists");
		//		return UnprocessableEntity(ModelState);
		//	}

		//	// Validate the email
		//	if (!_sharedService.IsValidGmail(userDto.Email))
		//	{
		//		ModelState.AddModelError("", "Invalid email address");
		//		return BadRequest(ModelState);
		//	}

		//	// Validate the model state
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	// Hash the password
		//	userDto.Password = _userService.HashPassword(userDto.Password);

		//	// Map DTO to User entity
		//	var user = _mapper.Map<User>(userDto);
		//	user.Created_At = DateTime.UtcNow;
		//	user.Modified_At = DateTime.UtcNow;

		//	// Create the user
		//	if (!_userService.CreateUser(user))
		//	{
		//		ModelState.AddModelError("", "Something went wrong creating user");
		//		return StatusCode(500, ModelState);
		//	}

		//	return Created("Successfully created user", user);
		//}
	}
}
