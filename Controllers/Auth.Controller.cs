using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/[controller]")]
	[ApiController]
	public class AuthController(
		IAuthService authService,
		JWTService jwtService,
		API_ResponseConvention api_Response
	) : ControllerBase
	{
		private readonly IAuthService _authService = authService;
		private readonly JWTService _jwtService = jwtService;
		private readonly API_ResponseConvention _api_Response = api_Response;

		[HttpPost("signin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public ActionResult<User> Signin([FromBody] SignInDTO item)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			User _user = _authService.AuthenticatedUser(item);

			if (_user == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(401, failedMessage);
			}

			string _token = _jwtService.GenerateJwtToken(_user);

			var okMessage = _api_Response.OkMessage(_token, methodName, _user);
			return StatusCode(200, okMessage);
		}
	}
}
