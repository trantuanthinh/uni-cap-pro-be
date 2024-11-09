using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
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
    public class FeedbacksController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        FeedbackService service,
        Sub_OrderService subOrderService,
        ProductService productService
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly FeedbackService _service = service;
        private readonly Sub_OrderService _subOrderService = subOrderService;
        private readonly ProductService _productService = productService;

        [HttpGet("product/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedbacks(Guid productId)
        {
            string methodName = nameof(GetFeedbacks);

            ICollection<FeedbackResponse> _items = await _service.GetFeedbacks(productId);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpPost("product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackRequest item)
        {
            string methodName = nameof(CreateFeedback);

            Feedback _item = _mapper.Map<Feedback>(item);
            bool isCreated = await _service.CreateFeedback(_item);
            if (!isCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            bool isUpdated_1 = await _subOrderService.UpdateSub_OrderRating(_item.Item_OrderId);
            if (!isUpdated_1)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }

            bool isUpdated_2 = await _productService.UpdateProductRating(
                _item.ProductId,
                _item.Rating
            );
            if (!isUpdated_2)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }
            var okMessage = _apiResponse.Success(methodName, item);
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
