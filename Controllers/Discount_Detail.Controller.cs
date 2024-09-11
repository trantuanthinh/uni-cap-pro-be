using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // TODO
    [Route("/[controller]")]
    [ApiController]
    public class Discount_DetailsController(
        IDiscount_DetailService service,
        IMapper mapper,
        API_ResponseConvention api_Response
    ) : ControllerBase
    {
        private readonly IDiscount_DetailService _service = service;
        private readonly IMapper _mapper = mapper;
        private readonly API_ResponseConvention _api_Response = api_Response;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDiscount_Details()
        {
            string methodName = nameof(GetDiscount_Details);

            ICollection<Discount_Detail> _items = await _service.GetDiscount_Details();

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
        public async Task<IActionResult> GetDiscount_Detail(Guid id)
        {
            string methodName = nameof(GetDiscount_Detail);

            Discount_Detail _item = await _service.GetDiscount_Detail(id);

            if (_item == null)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(404, failedMessage);
            }

            var okMessage = _api_Response.OkMessage(methodName, _item);
            return StatusCode(200, okMessage);
        }

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<IActionResult> CreateDiscount([FromBody] string item) // TODO
        // {
        //     string methodName = nameof(CreateDiscount);

        //     if (!ModelState.IsValid)
        //     {
        //         var failedMessage = _api_Response.FailedMessage(methodName, ModelState);
        //         return StatusCode(400, failedMessage);
        //     }

        //     Discount_Detail _item = _mapper.Map<Discount_Detail>(item);
        //     bool isCreated = await _service.CreateDiscount_Detail(_item);
        //     if (!isCreated)
        //     {
        //         var failedMessage = _api_Response.FailedMessage(methodName);
        //         return StatusCode(500, failedMessage);
        //     }

        //     var okMessage = _api_Response.OkMessage(methodName, _item);
        //     return StatusCode(200, okMessage);
        // }

        // [HttpPatch("{id:guid}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // public async Task<IActionResult> PatchDiscount(Guid id, [FromBody] string item) // TODO
        // {
        //     string methodName = nameof(PatchDiscount);

        //     Discount_Detail _item = await _service.GetDiscount_Detail(id);

        //     if (item == null || _item == null)
        //     {
        //         var failedMessage = _api_Response.FailedMessage(methodName);
        //         return StatusCode(404, failedMessage);
        //     }

        //     if (!TryValidateModel(_item))
        //     {
        //         return ValidationProblem(ModelState);
        //     }

        //     Discount_Detail patchItem = _mapper.Map<Discount_Detail>(item);
        //     bool isUpdated = await _service.UpdateDiscount_Detail(_item, patchItem);
        //     if (isUpdated)
        //     {
        //         var failedMessage = _api_Response.FailedMessage(methodName);
        //         return StatusCode(500, failedMessage);
        //     }

        //     var okMessage = _api_Response.OkMessage(methodName, _item);
        //     return StatusCode(200, okMessage);
        // }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            string methodName = nameof(DeleteDiscount);

            Discount_Detail _item = await _service.GetDiscount_Detail(id);

            if (_item == null)
            {
                var failedMessage = _api_Response.FailedMessage(methodName);
                return StatusCode(404, failedMessage);
            }

            bool isDeleted = await _service.DeleteDiscount_Detail(_item);
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
