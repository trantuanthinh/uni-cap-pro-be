﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class AuthController(
        JWTService jwtService,
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        AuthService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly AuthService _service = service;
        private readonly JWTService _jwtService = jwtService;

        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Signin([FromBody] SignInRequest item)
        {
            string methodName = nameof(Signin);

            User _user = _service.AuthenticatedUser(item);

            string _token = _jwtService.GenerateJwtToken(_user);

            var okMessage = _apiResponse.Success(_token, methodName, _user.ToResponse());
            return StatusCode(200, okMessage);
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] UserRequest item)
        {
            string methodName = nameof(SignUp);

            if (!_sharedService.IsValidGmail(item.Email))
            {
                ModelState.AddModelError("", "Invalid email address");
                var failedMessage = _apiResponse.Failure(methodName, ModelState);
                return StatusCode(400, failedMessage);
            }

            User _item = _mapper.Map<User>(item);
            bool isCreated = await _service.CreateUser(_item);
            if (!isCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        // 1: Send OTP, 2: Verify OTP, 3: Change Password
        [HttpPost("send-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendOTP([FromBody] OTPRequest item)
        {
            string methodName = nameof(SendOTP);

            if (!_sharedService.IsValidGmail(item.Email))
            {
                ModelState.AddModelError("", "Invalid Email address");
                var failedMessage = _apiResponse.Failure(methodName, ModelState);
                return StatusCode(400, failedMessage);
            }

            bool isSent = await _service.SendOTP(item);
            if (!isSent)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, true);
            return StatusCode(200, okMessage);
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest item)
        {
            string methodName = nameof(ResetPassword);

            if (!_sharedService.IsValidGmail(item.Email))
            {
                ModelState.AddModelError("", "Invalid email address");
                var failedMessage = _apiResponse.Failure(methodName, ModelState);
                return StatusCode(400, failedMessage);
            }

            bool isReset = await _service.ResetPassword(item);
            if (!isReset)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, true);
            return StatusCode(200, okMessage);
        }
    }
}
