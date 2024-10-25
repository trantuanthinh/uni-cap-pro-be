using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class FeedbacksController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        FeedbackService service,
        UserService userService,
        ProductService productService
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly FeedbackService _service = service;
        private readonly UserService _userService = userService;
        private readonly ProductService _productService = productService;

        [HttpGet("products/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedbacks(Guid productId)
        {
            string methodName = nameof(GetFeedbacks);

            ICollection<FeedbackResponse> _items = await _service.GetFeedbacks(productId);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpPost("products/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFeedback(
            Guid productId,
            [FromBody] FeedbackRequest item
        )
        {
            string methodName = nameof(CreateFeedback);

            UserResponse userResponse = await _userService.GetUser(item.UserId);
            if (userResponse == null)
            {
                var failedMessage = _apiResponse.Failure("User not found");
                return StatusCode(404, failedMessage);
            }

            ProductResponse productResponse = await _productService.GetProduct(productId);
            if (productResponse == null)
            {
                var failedMessage = _apiResponse.Failure("Product not found");
                return StatusCode(404, failedMessage);
            }

            Feedback _item = _mapper.Map<Feedback>(item);
            _item.ProductId = productId;
            bool isCreated = await _service.CreateFeedback(_item);
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
        public async Task<IActionResult> PatchFeedback(
            Guid id,
            [FromBody] PatchRequest<FeedbackRequest> patchRequest
        )
        {
            string methodName = nameof(PatchFeedback);

            bool isUpdated = await _service.UpdateFeedback(id, patchRequest);
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
        public async Task<IActionResult> DeleteFeedback(Guid id)
        {
            string methodName = nameof(DeleteFeedback);

            bool isDeleted = await _service.DeleteFeedback(id);
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
