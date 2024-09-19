// using AutoMapper;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using uni_cap_pro_be.Core;
// using uni_cap_pro_be.DTO.Request;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Repositories;

// namespace uni_cap_pro_be.Controllers
// {
//     // DONE
//     [Route("/[controller]")]
//     [ApiController]
//     public class Product_CategoriesController(
//         IProduct_CategoryService service,
//         IMapper mapper
//     // BaseResponse api_Response
//     ) : ControllerBase
//     {
//         private readonly IProduct_CategoryService _service = service;
//         private readonly IMapper _mapper = mapper;
//         // private readonly BaseResponse _api_Response = api_Response;

//         // [HttpGet]
//         // [ProducesResponseType(StatusCodes.Status200OK)]
//         // [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         // public async Task<IActionResult> GetProductCategories()
//         // {
//         // 	string methodName = nameof(GetProductCategories);

//         // 	ICollection<Product_Category> _items = await _service.GetProduct_Categories();

//         // 	if (!ModelState.IsValid)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName, ModelState);
//         // 		return StatusCode(400, failedMessage);
//         // 	}

//         // 	var okMessage = _apiResponse.Success(methodName, _items);
//         // 	return StatusCode(200, okMessage);
//         // }

//         // [HttpGet("{id:guid}")]
//         // [ProducesResponseType(StatusCodes.Status200OK)]
//         // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//         // [ProducesResponseType(StatusCodes.Status404NotFound)]
//         // public async Task<IActionResult> GetProductCategory(Guid id)
//         // {
//         // 	string methodName = nameof(GetProductCategory);

//         // 	Product_Category _item = await _service.GetProduct_Category(id);

//         // 	if (_item == null)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName);
//         // 		return StatusCode(404, failedMessage);
//         // 	}

//         // 	var okMessage = _apiResponse.Success(methodName, _item);
//         // 	return StatusCode(200, okMessage);
//         // }

//         // [HttpPost]
//         // [ProducesResponseType(StatusCodes.Status200OK)]
//         // [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
//         // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         // public async Task<IActionResult> CreateProductCategory(
//         // 	[FromBody] Product_CategoryCreateDTO item
//         // )
//         // {
//         // 	string methodName = nameof(CreateProductCategory);

//         // 	if (!ModelState.IsValid)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName, ModelState);
//         // 		return StatusCode(400, failedMessage);
//         // 	}

//         // 	Product_Category _item = _mapper.Map<Product_Category>(item);
//         // 	bool isCreated = await _service.CreateProduct_Category(_item);
//         // 	if (!isCreated)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName);
//         // 		return StatusCode(500, failedMessage);
//         // 	}

//         // 	var okMessage = _apiResponse.Success(methodName, ModelState);
//         // 	return StatusCode(200, okMessage);
//         // }

//         // [Authorize]
//         // [HttpPatch("{id:guid}")]
//         // [ProducesResponseType(StatusCodes.Status200OK)]
//         // [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         // [ProducesResponseType(StatusCodes.Status404NotFound)]
//         // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         // public async Task<IActionResult> PatchProductCategory(
//         // 	Guid id,
//         // 	[FromBody] Product_CategoryCreateDTO item
//         // )
//         // {
//         // 	string methodName = nameof(PatchProductCategory);

//         // 	Product_Category _item = await _service.GetProduct_Category(id);

//         // 	if (item == null || _item == null)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName);
//         // 		return StatusCode(404, failedMessage);
//         // 	}

//         // 	if (!TryValidateModel(_item))
//         // 	{
//         // 		return ValidationProblem(ModelState);
//         // 	}

//         // 	Product_Category patchItem = _mapper.Map<Product_Category>(item);
//         // 	bool isUpdated = await _service.UpdateProduct_Category(_item, patchItem);
//         // 	if (isUpdated)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName, ModelState);
//         // 		return StatusCode(500, failedMessage);
//         // 	}

//         // 	var okMessage = _apiResponse.Success(methodName, _item);
//         // 	return StatusCode(200, okMessage);
//         // }

//         // [HttpDelete("{id:guid}")]
//         // [ProducesResponseType(StatusCodes.Status200OK)]
//         // [ProducesResponseType(StatusCodes.Status404NotFound)]
//         // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         // public async Task<IActionResult> DeleteProductCategory(Guid id)
//         // {
//         // 	string methodName = nameof(DeleteProductCategory);

//         // 	Product_Category _item = await _service.GetProduct_Category(id);

//         // 	if (_item == null)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName);
//         // 		return StatusCode(404, failedMessage);
//         // 	}

//         // 	bool isDeleted = await _service.DeleteProduct_Category(_item);
//         // 	if (!isDeleted)
//         // 	{
//         // 		var failedMessage = _apiResponse.Failure(methodName);
//         // 		return StatusCode(500, failedMessage);
//         // 	}

//         // 	var responseMessage = _apiResponse.Success(methodName, _item);
//         // 	return StatusCode(200, responseMessage);
//         // }
//     }
// }
