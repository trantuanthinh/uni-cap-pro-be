using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Controllers
{
	[Route("/")]
	[ApiController]
	public class AuthController(IAuthService authService, JWTService jwtService) : Controller
	{
		private readonly IAuthService _authService = authService;
		private readonly JWTService _jwtService = jwtService;

		[HttpPost("auth/signin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public ActionResult<User> Signin([FromBody] SignInDTO item)
		{
			User _user = _authService.AuthenticatedUser(item);

			if (_user == null)
			{
				ModelState.AddModelError("", "Not Found - Invalid email/phone/username or password");
				return StatusCode(401, ModelState);
			}

			string _token = _jwtService.GenerateJwtToken(_user);

			return StatusCode(200, new { token = _token, data = _user });
		}
	}
}
