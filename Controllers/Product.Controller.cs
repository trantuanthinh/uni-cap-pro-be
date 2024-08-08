using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class ProductController(IProductService productService, IMapper mapper, SharedService sharedService) : Controller
	{
		private readonly IProductService _productService = productService;
		private readonly IMapper _mapper = mapper;
		private readonly SharedService _sharedService = sharedService;

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProducts()
		{
			ICollection<Product> _items = _productService.GetProducts();

			if (!ModelState.IsValid)
			{
				return StatusCode(400, ModelState);
			}
			return StatusCode(200, new { data = _items });
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetProduct(Guid id)
		{

			Product _item = _productService.GetProduct(id);

			if (_item == null)
			{
				return StatusCode(404, new { message = "Product not found." });
			}

			return StatusCode(200, new { data = _item });
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CreateProduct([FromBody] ProductDTO item)
		{
			if (!ModelState.IsValid)
			{
				return StatusCode(400, ModelState);
			}

			Product _item = _mapper.Map<Product>(item);
			bool isCreated = _productService.CreateProduct(_item);
			if (!isCreated)
			{
				ModelState.AddModelError("", "Invalid. Something went wrong creating Product.");
				return StatusCode(500, ModelState);
			}
			return StatusCode(200, new { message = "Created Successfully", data = _item });
		}

		[Authorize]
		[HttpPatch("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult PatchProduct(Guid id, [FromBody] ProductDTO item)
		{
			Product _item = _productService.GetProduct(id);

			if (item == null || _item == null)
			{
				return StatusCode(404, ModelState);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			Product patchItem = _mapper.Map<Product>(item);
			if (!_productService.UpdateProduct(_item, patchItem))
			{
				ModelState.AddModelError("", "Invalid - Something went wrong updating the Product");
				return StatusCode(500, ModelState);
			}

			return StatusCode(200, new { message = "Updated Successfully", data = _item });
		}

		[HttpDelete("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteProduct(Guid id)
		{
			Product _item = _productService.GetProduct(id);

			if (_item == null)
			{
				return StatusCode(404, new { message = "Product not found" });
			}

			bool isDeleted = _productService.DeleteProduct(_item);

			if (!isDeleted)
			{
				return StatusCode(500, new { message = "An error occurred while deleting the Product" });
			}

			return StatusCode(202, new { message = "Deleted Successfully" });
		}
	}
}
