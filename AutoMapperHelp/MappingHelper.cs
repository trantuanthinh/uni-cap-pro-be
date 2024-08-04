using AutoMapper;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.AutoMapperHelp
{
	public class MappingHelper : Profile
	{
		public MappingHelper()
		{
			CreateMap<User, UserDTO>();
			CreateMap<UserDTO, User>();
		}

	}
}
