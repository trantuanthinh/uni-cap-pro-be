using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Core
{
    public class BaseAPIController : ControllerBase
    {
        protected readonly DataContext _dataContext;
        protected readonly IMapper _mapper;
        protected readonly SharedService _sharedService;
        protected readonly APIResponse _apiResponse;

        public BaseAPIController() { }

        public BaseAPIController(
            DataContext dataContext,
            IMapper mapper,
            APIResponse apiResponse,
            SharedService sharedService
        )
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _apiResponse = apiResponse;
            _sharedService = sharedService;
        }
    }
}
