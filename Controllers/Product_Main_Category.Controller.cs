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
    public class Product_Main_CategoriesController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        Product_Main_CategoryService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly Product_Main_CategoryService _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct_Main_Categories(
            [FromQuery] QueryParameters queryParameters
        )
        {
            string methodName = nameof(GetProduct_Main_Categories);

            queryParameters.SortBy = "Name";
            queryParameters.SortOrder = "asc";

            BaseResponse<Product_Main_CategoryResponse> _items =
                await _service.GetProduct_Main_Categories(queryParameters);
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct_Main_Category(Guid id)
        {
            string methodName = nameof(GetProduct_Main_Category);

            Product_Main_CategoryResponse _item = await _service.GetProduct_Main_Category(id);

            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct_Main_Category(
            [FromBody] Product_Main_CategoryRequest item
        )
        {
            string methodName = nameof(CreateProduct_Main_Category);

            Product_Main_Category _item = _mapper.Map<Product_Main_Category>(item);
            bool isCreated = await _service.CreateProduct_Main_Category(_item);
            if (!isCreated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        // [Authorize]
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchProduct_Main_Category(
            Guid id,
            [FromBody] PatchRequest<Product_Main_CategoryRequest> item
        )
        {
            string methodName = nameof(PatchProduct_Main_Category);

            bool isUpdated = await _service.UpdateProduct_Main_Category(id, item);
            if (isUpdated)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _apiResponse.Success(methodName, item);
            return StatusCode(200, okMessage);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct_Main_Category(Guid id)
        {
            string methodName = nameof(DeleteProduct_Main_Category);

            bool isDeleted = await _service.DeleteProduct_Main_Category(id);
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
