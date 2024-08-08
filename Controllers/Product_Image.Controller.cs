using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Controllers
{
	[Route("/")]
	[ApiController]
	public class Product_ImageController(IProduct_ImageService product_ImageService, IMapper mapper) : Controller
	{
		private readonly IProduct_ImageService _product_ImageService = product_ImageService;
		private readonly IMapper _mapper = mapper;

		[HttpGet("product_images")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProductImageImages()
		{
			ICollection<Product_Image> _items = _product_ImageService.GetProduct_Images();

			if (!ModelState.IsValid)
			{
				return StatusCode(400, ModelState);
			}
			return StatusCode(200, new { data = _items });
		}

		[HttpGet("product_image/{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetProductImage(Guid id)
		{

			Product_Image _item = _product_ImageService.GetProduct_Image(id);

			if (_item == null)
			{
				return StatusCode(404, new { message = "Product_Image not found." });
			}

			return StatusCode(200, new { data = _item });
		}

		[HttpPost("product_image")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CreateProduct_Image([FromBody] Product_ImageDTO item)
		{
			if (!ModelState.IsValid)
			{
				return StatusCode(400, ModelState);
			}

			Product_Image _item = _mapper.Map<Product_Image>(item);
			bool isCreated = _product_ImageService.CreateProduct_Image(_item);
			if (!isCreated)
			{
				ModelState.AddModelError("", "Invalid. Something went wrong creating Product_Image.");
				return StatusCode(500, ModelState);
			}
			return StatusCode(200, new { message = "Created Successfully", data = _item });
		}

		[Authorize]
		[HttpPatch("product_image/{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult PatchProduct_Image(Guid id, [FromBody] Product_ImageDTO item)
		{
			Product_Image _item = _product_ImageService.GetProduct_Image(id);

			if (item == null || _item == null)
			{
				return StatusCode(404, ModelState);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			Product_Image patchItem = _mapper.Map<Product_Image>(item);
			if (!_product_ImageService.UpdateProduct_Image(_item, patchItem))
			{
				ModelState.AddModelError("", "Invalid - Something went wrong updating the Product_Image");
				return StatusCode(500, ModelState);
			}

			return StatusCode(200, new { message = "Updated Successfully", data = _item });
		}

		[HttpDelete("product_image/{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteProduct_Image(Guid id)
		{
			Product_Image _item = _product_ImageService.GetProduct_Image(id);

			if (_item == null)
			{
				return StatusCode(404, new { message = "Product_Image not found" });
			}

			bool isDeleted = _product_ImageService.DeleteProduct_Image(_item);

			if (!isDeleted)
			{
				return StatusCode(500, new { message = "An error occurred while deleting the Product_Image" });
			}

			return StatusCode(202, new { message = "Deleted Successfully" });
		}
	}
}
