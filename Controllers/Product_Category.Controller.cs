using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    [Route("/")]
    [ApiController]
    public class ProductCategoryController(
        IProduct_CategoryService product_CategoryService,
        IMapper mapper,
        API_ResponseConvention api_Response
    ) : Controller
    {
        private readonly IProduct_CategoryService _product_CategoryService =
            product_CategoryService;
        private readonly IMapper _mapper = mapper;
        private readonly API_ResponseConvention _api_Response = api_Response;

        [HttpGet("product_categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProductCategories()
        {
            ICollection<Product_Category> _items = _product_CategoryService.GetProduct_Categories();

            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            string methodName = MethodBase.GetCurrentMethod().Name;
            var responseMessage = _api_Response.ResponseMessage(
                $"{methodName} Successfully",
                _items
            );
            return StatusCode(200, responseMessage);
        }

        [HttpGet("product_category/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductCategory(Guid id)
        {
            Product_Category _item = _product_CategoryService.GetProduct_Category(id);

            if (_item == null)
            {
                return StatusCode(404, new { message = "Product_Category not found." });
            }

            string methodName = MethodBase.GetCurrentMethod().Name;
            var responseMessage = _api_Response.ResponseMessage(
                $"{methodName} Successfully",
                _item
            );
            return StatusCode(200, responseMessage);
        }

        [HttpPost("product_category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProductCategory([FromBody] Product_CategoryDTO item)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, ModelState);
            }

            Product_Category _item = _mapper.Map<Product_Category>(item);
            bool isCreated = _product_CategoryService.CreateProduct_Category(_item);
            if (!isCreated)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid. Something went wrong creating Product_Category."
                );
                return StatusCode(500, ModelState);
            }

            string methodName = MethodBase.GetCurrentMethod().Name;
            var responseMessage = _api_Response.ResponseMessage(
                $"{methodName} Successfully",
                _item
            );
            return StatusCode(200, responseMessage);
        }

        [Authorize]
        [HttpPatch("product_category/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PatchProductCategory(Guid id, [FromBody] Product_CategoryDTO item)
        {
            Product_Category _item = _product_CategoryService.GetProduct_Category(id);

            if (item == null || _item == null)
            {
                return StatusCode(404, ModelState);
            }

            if (!TryValidateModel(_item))
            {
                return ValidationProblem(ModelState);
            }

            Product_Category patchItem = _mapper.Map<Product_Category>(item);
            if (!_product_CategoryService.UpdateProduct_Category(_item, patchItem))
            {
                ModelState.AddModelError(
                    "",
                    "Invalid - Something went wrong updating the Product_Category"
                );
                return StatusCode(500, ModelState);
            }

            string methodName = MethodBase.GetCurrentMethod().Name;
            var responseMessage = _api_Response.ResponseMessage(
                $"{methodName} Successfully",
                _item
            );
            return StatusCode(200, responseMessage);
        }

        [HttpDelete("product_category/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProductCategory(Guid id)
        {
            Product_Category _item = _product_CategoryService.GetProduct_Category(id);

            if (_item == null)
            {
                return StatusCode(404, new { message = "Product_Category not found" });
            }

            bool isDeleted = _product_CategoryService.DeleteProduct_Category(_item);

            if (!isDeleted)
            {
                return StatusCode(
                    500,
                    new { message = "An error occurred while deleting the Product_Category" }
                );
            }

            string methodName = MethodBase.GetCurrentMethod().Name;
            var responseMessage = _api_Response.ResponseMessage(
                $"{methodName} Successfully",
                _item
            );
            return StatusCode(200, responseMessage);
        }
    }
}
