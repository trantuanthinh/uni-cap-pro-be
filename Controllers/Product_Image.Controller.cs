// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using uni_cap_pro_be.Core;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Repositories;

// namespace uni_cap_pro_be.Controllers
// {
// 	// DONE
// 	[Route("[controller]")]
// 	[ApiController]
// 	public class Product_ImagesController(
// 		IProduct_ImageService service,
// 		IMapper mapper
// 		// BaseResponse api_Response
// 	) : ControllerBase
// 	{
// 		private readonly IProduct_ImageService _service = service;
// 		private readonly IMapper _mapper = mapper;
// 		// private readonly BaseResponse _api_Response = api_Response;

// 		// [HttpGet("{name}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> GetProduct_Image(Guid ownerId, string name)
// 		// {
// 		// 	var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources", name);

// 		// 	if (!System.IO.File.Exists(filePath))
// 		// 	{
// 		// 		return StatusCode(404, new { message = "Image not found!" });
// 		// 	}

// 		// 	try
// 		// 	{
// 		// 		var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

// 		// 		var extension = Path.GetExtension(name).ToLowerInvariant();
// 		// 		string contentType = extension switch
// 		// 		{
// 		// 			".jpg" or ".jpeg" => "image/jpeg",
// 		// 			".png" => "image/png",
// 		// 			".gif" => "image/gif",
// 		// 			".bmp" => "image/bmp",
// 		// 			_ => "application/octet-stream",
// 		// 		};
// 		// 		return File(fileBytes, contentType);
// 		// 	}
// 		// 	catch (Exception ex)
// 		// 	{
// 		// 		return StatusCode(
// 		// 			500,
// 		// 			new { message = "Unable to retrieve image!", error = ex.Message }
// 		// 		);
// 		// 	}
// 		// }

// 		// [HttpPost]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> CreateProduct_Image(Product_ImageDTO item, IFormFile file)
// 		// {
// 		// 	string methodName = nameof(CreateProduct_Image);

// 		// 	try
// 		// 	{
// 		// 		// Kiểm tra file có hợp lệ hay không
// 		// 		if (file == null || file.Length == 0)
// 		// 		{
// 		// 			var failedMessage = _api_Response.FailedMessage(
// 		// 				methodName,
// 		// 				"File is null or empty"
// 		// 			);
// 		// 			return StatusCode(400, failedMessage);
// 		// 		}

// 		// 		var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
// 		// 		var fileExtension = Path.GetExtension(file.FileName).ToLower();
// 		// 		if (!validExtensions.Contains(fileExtension))
// 		// 		{
// 		// 			var failedMessage = _api_Response.FailedMessage(
// 		// 				methodName,
// 		// 				"Invalid file format"
// 		// 			);
// 		// 			return StatusCode(400, failedMessage);
// 		// 		}

// 		// 		string _uploadFolderPath = Path.Combine(
// 		// 			Directory.GetCurrentDirectory(),
// 		// 			$"Resources/{item.OwnerId}"
// 		// 		);

// 		// 		if (!Directory.Exists(_uploadFolderPath))
// 		// 		{
// 		// 			Directory.CreateDirectory(_uploadFolderPath);
// 		// 		}

// 		// 		var filePath = Path.Combine(_uploadFolderPath, file.FileName);

// 		// 		using (var stream = new FileStream(filePath, FileMode.Create))
// 		// 		{
// 		// 			await file.CopyToAsync(stream);
// 		// 		}

// 		// 		item.Name = file.FileName;

// 		// 		Product_Image _item = _mapper.Map<Product_Image>(item);

// 		// 		bool isCreated = await _service.CreateImage(_item);
// 		// 		if (!isCreated)
// 		// 		{
// 		// 			var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 			return StatusCode(500, failedMessage);
// 		// 		}

// 		// 		// Phản hồi thành công
// 		// 		var okMessage = _api_Response.OkMessage(methodName, _item);
// 		// 		return StatusCode(200, okMessage);
// 		// 	}
// 		// 	catch (Exception ex)
// 		// 	{
// 		// 		// Xử lý ngoại lệ
// 		// 		var errorMessage = _api_Response.FailedMessage(methodName, ex.Message);
// 		// 		return StatusCode(500, errorMessage);
// 		// 	}
// 		// }

// 		// [HttpDelete("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> DeleteProduct_Image(Guid id, Guid ownerId)
// 		// {
// 		// 	string methodName = nameof(DeleteProduct_Image);

// 		// 	Product_Image _item = await _service.GetImage(id);

// 		// 	if (_item == null)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(404, failedMessage);
// 		// 	}

// 		// 	string _uploadFolderPath = Path.Combine(
// 		// 		Directory.GetCurrentDirectory(),
// 		// 		$"Resources/{_item.Product.OwnerId}"
// 		// 	);

// 		// 	try
// 		// 	{
// 		// 		if (Directory.Exists(_uploadFolderPath))
// 		// 		{
// 		// 			var filePath = Path.Combine(_uploadFolderPath, _item.Name);
// 		// 			if (System.IO.File.Exists(filePath))
// 		// 			{
// 		// 				System.IO.File.Delete(filePath);
// 		// 			}
// 		// 		}

// 		// 		bool isDeleted = await _service.DeleteImage(_item);

// 		// 		if (!isDeleted)
// 		// 		{
// 		// 			var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 			return StatusCode(500, failedMessage);
// 		// 		}

// 		// 		var okMessage = _api_Response.OkMessage(methodName, _item);
// 		// 		return StatusCode(200, okMessage);
// 		// 	}
// 		// 	catch (Exception ex)
// 		// 	{
// 		// 		var errorMessage = _api_Response.FailedMessage(methodName, ex.Message);
// 		// 		return StatusCode(500, errorMessage);
// 		// 	}
// 		// }
// 	}
// }
