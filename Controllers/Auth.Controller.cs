using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class Auth(IUserService userService, IMapper mapper, SharedService sharedService)
	{
		private readonly IUserService _userService = userService;
		private readonly IMapper _mapper = mapper;
		private readonly SharedService _sharedService = sharedService;
	}
}
