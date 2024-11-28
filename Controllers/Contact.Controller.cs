using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.Middleware;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class ContactController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        MailService mailService
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly MailService _mailService = mailService;

        [HttpPost("send-contact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendContact([FromBody] ContactRequest item)
        {
            string methodName = nameof(SendContact);

            if (!_sharedService.IsValidGmail(item.Email))
            {
                ModelState.AddModelError("", "Invalid Email address");
                var failedMessage = _apiResponse.Failure(methodName, ModelState);
                return StatusCode(400, failedMessage);
            }

            var body = $@"{item.Email + "\n" + item.Subject + "\n" + item.Message}";

            bool isSent = await _mailService.SendMail(item.Email, item.Subject, body);
            if (!isSent)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, true);
            return StatusCode(200, okMessage);
        }
    }
}
