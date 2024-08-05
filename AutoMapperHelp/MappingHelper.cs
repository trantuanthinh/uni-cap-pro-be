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

			CreateMap<User, SignInDTO>()
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
				.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
		}

	}
}
