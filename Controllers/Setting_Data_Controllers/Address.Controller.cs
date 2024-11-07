using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Services.Setting_Data_Services;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers.Setting_Data_Controllers
{
    // DONE
    [Route("/[controller]")]
    [ApiController]
    public class AddressController(
        DataContext dataContext,
        IMapper mapper,
        APIResponse apiResponse,
        SharedService sharedService,
        AddressService service
    ) : BaseAPIController(dataContext, mapper, apiResponse, sharedService)
    {
        private readonly AddressService _service = service;

        [HttpGet("provinces")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProvinces()
        {
            string methodName = nameof(GetProvinces);

            ICollection<ProvinceResponse> _items = await _service.GetProvinces();
            var okMessage = _apiResponse.Success(methodName, _items);
            return StatusCode(200, okMessage);
        }

        [HttpGet("districts/{provinceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDistricts(string provinceId)
        {
            string methodName = nameof(GetDistricts);

            ICollection<DistrictResponse> _item = await _service.GetDistrictsByPId(provinceId);

            if (_item == null)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }
            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }

        [HttpGet("wards/{districtId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWards(string districtId)
        {
            string methodName = nameof(GetWards);

            ICollection<WardResponse> _item = await _service.GetWardsByDId(districtId);

            if (_item == null)
            {
                var failedMessage = _apiResponse.Failure(methodName);
                return StatusCode(404, failedMessage);
            }
            var okMessage = _apiResponse.Success(methodName, _item);
            return StatusCode(200, okMessage);
        }
    }
}
