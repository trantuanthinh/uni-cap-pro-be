﻿using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class Product_CategoriesController(
        IProduct_CategoryService product_CategoryService,
        IMapper mapper,
        API_ResponseConvention api_Response
    ) : Controller
    {
        private readonly IProduct_CategoryService _product_CategoryService =
            product_CategoryService;
        private readonly IMapper _mapper = mapper;
        private readonly API_ResponseConvention _api_Response = api_Response;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProductCategories()
        {
            string methodName = MethodBase.GetCurrentMethod().Name;

            ICollection<Product_Category> _items = _product_CategoryService.GetProduct_Categories();

            if (!ModelState.IsValid)
            {
                var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
                return StatusCode(400, failedMessage);
            }

            var okMessage = _api_Response.OkMessage(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductCategory(Guid id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;

            Product_Category _item = _product_CategoryService.GetProduct_Category(id);

            if (_item == null)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(404, failedMessage);
            }

            var okMessage = _api_Response.OkMessage(methodName, _item);
            return StatusCode(200, okMessage);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProductCategory([FromBody] Product_CategoryDTO item)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;

            if (!ModelState.IsValid)
            {
                var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
                return StatusCode(400, failedMessage);
            }

            Product_Category _item = _mapper.Map<Product_Category>(item);
            bool isCreated = _product_CategoryService.CreateProduct_Category(_item);
            if (!isCreated)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _api_Response.OkMessage(methodName, ModelState);
            return StatusCode(200, okMessage);
        }

        [Authorize]
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PatchProductCategory(Guid id, [FromBody] Product_CategoryDTO item)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;

            Product_Category _item = _product_CategoryService.GetProduct_Category(id);

            if (item == null || _item == null)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(404, failedMessage);
            }

            if (!TryValidateModel(_item))
            {
                return ValidationProblem(ModelState);
            }

            Product_Category patchItem = _mapper.Map<Product_Category>(item);
            if (!_product_CategoryService.UpdateProduct_Category(_item, patchItem))
            {
                var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
                return StatusCode(500, failedMessage);
            }

            var okMessage = _api_Response.OkMessage(methodName, _item);
            return StatusCode(200, okMessage);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProductCategory(Guid id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;

            Product_Category _item = _product_CategoryService.GetProduct_Category(id);

            if (_item == null)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(404, failedMessage);
            }

            bool isDeleted = _product_CategoryService.DeleteProduct_Category(_item);

            if (!isDeleted)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(500, failedMessage);
            }

            var responseMessage = _api_Response.OkMessage(methodName, _item);
            return StatusCode(200, responseMessage);
        }
    }
}