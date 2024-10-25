using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class DiscountsController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        DiscountService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly DiscountService _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDiscounts([FromQuery] QueryParameters queryParameters)
        {
            string methodName = nameof(GetDiscounts);

            BaseResponse<DiscountResponse> _items = await _service.GetDiscounts(queryParameters);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDiscount(Guid id)
        {
            string methodName = nameof(GetDiscount);

            DiscountResponse _item = await _service.GetDiscount(id);

            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        // [Authorize]
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchDiscount(
            Guid id,
            [FromBody] PatchRequest<DiscountRequest> patchRequest
        )
        {
            string methodName = nameof(PatchDiscount);

            bool isUpdated = await _service.UpdateDiscount(id, patchRequest);
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
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            string methodName = nameof(DeleteDiscount);

            bool isDeleted = await _service.DeleteDiscount(id);
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
