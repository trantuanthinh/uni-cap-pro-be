using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    [Route("/")]
    [ApiController]
    public class AuthController(
        IAuthService authService,
        JWTService jwtService,
        API_ResponseConvention api_Response
    ) : Controller
    {
        private readonly IAuthService _authService = authService;
        private readonly JWTService _jwtService = jwtService;
        private readonly API_ResponseConvention _api_Response = api_Response;

        [HttpPost("auth/signin")]
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

        // [HttpPost("auth/signup")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // public ActionResult<User> Signup([FromBody] UserDTO item)
        // {
        //     if (!_sharedService.IsValidGmail(item.Email))
        //     {
        //         ModelState.AddModelError("", "Invalid email address");
        //         return StatusCode(400, ModelState);
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return StatusCode(400, ModelState);
        //     }

        //     User _item = _mapper.Map<User>(item);
        //     bool isCreated = _userService.CreateUser(_item);
        //     if (!isCreated)
        //     {
        //         ModelState.AddModelError("", "Invalid. Something went wrong creating User.");
        //         return StatusCode(500, ModelState);
        //     }

        //     string methodName = MethodBase.GetCurrentMethod().Name;
        //     // var responseMessage = _api_Response.ResponseMessage(
        //     //     $"{methodName} Successfully",
        //     //     _token,
        //     //     _user
        //     // );
        //     // return StatusCode(200, responseMessage);
        // }
    }
}
