using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class Product_ImagesController(IBaseService<Product_Image> service, IMapper mapper, API_ResponseConvention api_Response) : ControllerBase
	{
		private readonly IBaseService<Product_Image> _service = service;
		private readonly IMapper _mapper = mapper;
		private readonly API_ResponseConvention _api_Response = api_Response;

		private string _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetProductImageImages()
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			ICollection<Product_Image> _items = _service.GetItems();

			if (!ModelState.IsValid)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(400, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _items);
			return StatusCode(200, okMessage);
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetProductImage(Guid id)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			Product_Image _item = _service.GetItem(id);

			if (_item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateProduct_Image([FromBody] Product_ImageDTO item, IFormFile file)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;
			var ownerId = item.OwnerId;
			var productId = item.ProductId;
			_uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources/{ownerId}/{productId}");

			if (!Directory.Exists(_uploadFolderPath))
			{
				Directory.CreateDirectory(_uploadFolderPath);
			}

			if (file == null || file.Length == 0)
			{
				var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
				return StatusCode(400, failedMessage);
			}

			var filePath = Path.Combine(_uploadFolderPath, file.FileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			Product_Image _item = _mapper.Map<Product_Image>(item);
			bool isCreated = _service.CreateItem(_item);
			if (!isCreated)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}
		[Authorize]
		[HttpPatch("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult PatchProduct_Image(Guid id, [FromBody] Product_ImageDTO item)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			Product_Image _item = _service.GetItem(id);

			if (item == null || _item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			if (!TryValidateModel(_item))
			{
				return ValidationProblem(ModelState);
			}

			Product_Image patchItem = _mapper.Map<Product_Image>(item);
			if (!_service.UpdateItem(_item, patchItem))
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}

		[HttpDelete("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteProduct_Image(Guid id)
		{
			string methodName = MethodBase.GetCurrentMethod().Name;

			Product_Image _item = _service.GetItem(id);

			if (_item == null)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(404, failedMessage);
			}

			bool isDeleted = _service.DeleteItem(_item);

			if (!isDeleted)
			{
				var failedMessage = _api_Response.FailedMessage(methodName);
				return StatusCode(500, failedMessage);
			}

			var okMessage = _api_Response.OkMessage(methodName, _item);
			return StatusCode(200, okMessage);
		}
	}
}
