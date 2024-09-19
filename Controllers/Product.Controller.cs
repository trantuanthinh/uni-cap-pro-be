// using AutoMapper;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using uni_cap_pro_be.Core;
// using uni_cap_pro_be.DTO.Request;
// using uni_cap_pro_be.DTO.Response;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Repositories;

// namespace uni_cap_pro_be.Controllers
// {
// 	// DONE
// 	[Route("/[controller]")]
// 	[ApiController]
// 	public class ProductsController(
// 		IProductService service,
// 		IProduct_CategoryService categoryService,
// 		IProduct_ImageService imageSerivce,
// 		IMapper mapper
// 		// BaseResponse api_Response
// 	) : ControllerBase
// 	{
// 		private readonly IProductService _service = service;
// 		private readonly IProduct_CategoryService _categoryService = categoryService;
// 		private readonly IProduct_ImageService _imageSerivce = imageSerivce;
// 		private readonly IMapper _mapper = mapper;
// 		// private readonly BaseResponse _api_Response = api_Response;

// 		// [HttpGet]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
// 		// public async Task<IActionResult> GetProducts()
// 		// {
// 		// 	string methodName = nameof(GetProducts);

// 		// 	ICollection<Product> _items = await _service.GetProducts();

// 		// 	if (!ModelState.IsValid)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
// 		// 		return StatusCode(400, failedMessage);
// 		// 	}

// 		// 	var _dtos = new List<ProductDTO>();
// 		// 	foreach (var item in _items)
// 		// 	{
// 		// 		ProductDTO _item = _mapper.Map<ProductDTO>(item);
// 		// 		_dtos.Add(_item);
// 		// 	}

// 		// 	var okMessage = _api_Response.OkMessage(methodName, _dtos);
// 		// 	return StatusCode(200, okMessage);
// 		// }

// 		// [HttpGet("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status401Unauthorized)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// public async Task<IActionResult> GetProduct(Guid id)
// 		// {
// 		// 	string methodName = nameof(GetProduct);

// 		// 	Product _item = await _service.GetProduct(id);

// 		// 	if (_item == null)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(404, failedMessage);
// 		// 	}

// 		// 	ProductDTO dto = _mapper.Map<ProductDTO>(_item);

// 		// 	var okMessage = _api_Response.OkMessage(methodName, dto);
// 		// 	return StatusCode(200, okMessage);
// 		// }

// 		// [HttpPost]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
// 		// [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO item)
// 		// {
// 		// 	string methodName = nameof(CreateProduct);

// 		// 	if (!ModelState.IsValid)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
// 		// 		return StatusCode(400, failedMessage);
// 		// 	}

// 		// 	Product _item = _mapper.Map<Product>(item);
// 		// 	bool isCreated = await _service.CreateProduct(_item);
// 		// 	if (!isCreated)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(500, failedMessage);
// 		// 	}

// 		// 	var okMessage = _api_Response.OkMessage(methodName, _item);
// 		// 	return StatusCode(200, okMessage);
// 		// }

// 		// [Authorize]
// 		// [HttpPatch("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status400BadRequest)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> PatchProduct(Guid id, [FromBody] ProductCreateDTO item)
// 		// {
// 		// 	string methodName = nameof(PatchProduct);

// 		// 	Product _item = await _service.GetProduct(id);

// 		// 	if (item == null || _item == null)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(404, failedMessage);
// 		// 	}

// 		// 	if (!TryValidateModel(_item))
// 		// 	{
// 		// 		return ValidationProblem(ModelState);
// 		// 	}

// 		// 	Product patchItem = _mapper.Map<Product>(item);
// 		// 	bool isUpdated = await _service.UpdateProduct(_item, patchItem);
// 		// 	if (isUpdated)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(500, failedMessage);
// 		// 	}

// 		// 	var okMessage = _api_Response.OkMessage(methodName, _item);
// 		// 	return StatusCode(200, okMessage);
// 		// }

// 		// [HttpDelete("{id:guid}")]
// 		// [ProducesResponseType(StatusCodes.Status200OK)]
// 		// [ProducesResponseType(StatusCodes.Status404NotFound)]
// 		// [ProducesResponseType(StatusCodes.Status500InternalServerError)]
// 		// public async Task<IActionResult> DeleteProduct(Guid id)
// 		// {
// 		// 	string methodName = nameof(DeleteProduct);

// 		// 	Product _item = await _service.GetProduct(id);

// 		// 	if (_item == null)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(404, failedMessage);
// 		// 	}

// 		// 	bool isDeleted = await _service.DeleteProduct(_item);
// 		// 	if (!isDeleted)
// 		// 	{
// 		// 		var failedMessage = _api_Response.FailedMessage(methodName);
// 		// 		return StatusCode(500, failedMessage);
// 		// 	}

// 		// 	var okMessage = _api_Response.OkMessage(methodName, _item);
// 		// 	return StatusCode(200, okMessage);
// 		// }
// 	}
// }
