using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class CommentsController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        CommentService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly CommentService _service = service;

        [HttpGet("products/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComments(Guid productId)
        {
            string methodName = nameof(GetComments);

            ICollection<CommentResponse> _items = await _service.GetComments(productId);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpPost("products/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateComment(
            Guid productId,
            [FromBody] CommentRequest item
        )
        {
            string methodName = nameof(CreateComment);

            Comment _item = _mapper.Map<Comment>(item);
            bool isCreated = await _service.CreateComment(_item);
            if (!isCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        // [Authorize]
        [HttpPatch("products/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchComment(
            Guid id,
            [FromBody] PatchRequest<CommentRequest> patchRequest
        )
        {
            string methodName = nameof(PatchComment);

            bool isUpdated = await _service.UpdateComment(id, patchRequest);
            if (!isUpdated)
            {
                var failedMessage = _apiResponse.Failure(methodName, ModelState);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success($"{methodName} Successfully", id);
            return StatusCode(200, okMessage);
        }

        // [Authorize]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            string methodName = nameof(DeleteComment);

            bool isDeleted = await _service.DeleteComment(id);
            if (!isDeleted)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, id);
            return StatusCode(200, okMessage);
        }
    }
}
