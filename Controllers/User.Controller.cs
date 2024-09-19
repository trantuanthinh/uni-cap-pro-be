using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class UsersController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        UserService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly UserService _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers([FromQuery] QueryParameters queryParameters)
        {
            string methodName = nameof(GetUsers);

            BaseResponse<UserResponse> _items = await _service.GetUsers(queryParameters);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            string methodName = nameof(GetUser);

            UserResponse _item = await _service.GetUser(id);

            if (_item == null)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }
            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO item)
        //{
        //	string methodName = nameof(CreateUser);

        //	if (!_sharedService.IsValidGmail(item.Email))
        //	{
        //		ModelState.AddModelError("", "Invalid email address");
        //		var failedMessage = _apiResponse.Failure(methodName, ModelState);
        //		return StatusCode(400, failedMessage);
        //	}

        //	if (!ModelState.IsValid)
        //	{
        //		var failedMessage = _apiResponse.Failure(methodName, ModelState);
        //		return StatusCode(400, failedMessage);
        //	}

        //	User _item = _mapper.Map<User>(item);
        //	bool isCreated = await _service.CreateUser(_item);
        //	if (!isCreated)
        //	{
        //		var failedMessage = _apiResponse.Failure(methodName);
        //		return StatusCode(500, failedMessage);
        //	}

        //	var okMessage = _apiResponse.Success(methodName, _item);
        //	return StatusCode(200, okMessage);
        //}

        // [Authorize]
        // [HttpPatch("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PatchUser(Guid id, [FromBody] UserCreateDTO item)
        //{
        //	string methodName = nameof(PatchUser);
        //	User _item = await _service.GetUser(id);
        //	if (_item == null)
        //	{
        //		var failedMessage = _apiResponse.Failure(methodName);
        //		return StatusCode(404, failedMessage);
        //	}

        //	User patchItem = _mapper.Map<User>(item);
        //	bool isUpdated = await _service.UpdateUser(_item, patchItem);
        //	if (isUpdated)
        //	{
        //		ModelState.AddModelError("", "Invalid - Something went wrong updating the User");
        //		var failedMessage = _apiResponse.Failure(methodName, ModelState);
        //		return StatusCode(500, failedMessage);
        //	}

        //	var okMessage = _apiResponse.Success($"{methodName} Successfully", patchItem);
        //	return StatusCode(200, okMessage);
        //}

        // [Authorize]
        // [HttpDelete("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<IActionResult> DeleteUser(Guid id)
        // {
        // 	string methodName = nameof(DeleteUser);

        // 	User _item = await _service.GetUser(id);
        // 	if (_item == null)
        // 	{
        // 		var failedMessage = _apiResponse.Failure(methodName);
        // 		return StatusCode(404, failedMessage);
        // 	}

        // 	bool isDeleted = await _service.DeleteUser(_item);
        // 	if (!isDeleted)
        // 	{
        // 		var failedMessage = _apiResponse.Failure(methodName);
        // 		return StatusCode(500, failedMessage);
        // 	}

        // 	var okMessage = _apiResponse.Success(methodName, _item);
        // 	return StatusCode(200, okMessage);
        // }
    }
}
