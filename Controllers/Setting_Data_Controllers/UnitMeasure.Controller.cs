using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Services.Setting_Data_Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
    // TODO
    [Route("/[controller]")]
    [ApiController]
    public class UnitMeasureController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        UnitMeasureService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly UnitMeasureService _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitMeasures()
        {
            string methodName = nameof(GetUnitMeasures);

            ICollection<UnitMeasure> _items = await _service.GetUnitMeasures();
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUnitMeasure(Guid id)
        {
            string methodName = nameof(GetUnitMeasure);

            UnitMeasure _item = await _service.GetUnitMeasure(id);

            if (_item == null)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }
            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUnitMeasure(Guid id)
        {
            string methodName = nameof(DeleteUnitMeasure);

            bool isDeleted = await _service.DeleteUnitMeasure(id);
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
