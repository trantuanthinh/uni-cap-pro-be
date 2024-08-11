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

			CreateMap<Product, ProductDTO>();
			CreateMap<ProductDTO, Product>();

			CreateMap<Product_Category, Product_CategoryDTO>();
			CreateMap<Product_CategoryDTO, Product_Category>();

			CreateMap<Product_Image, Product_ImageDTO>().ForMember(dest => dest.OwnerId, opt => opt.Ignore());
			CreateMap<Product_ImageDTO, Product_Image>();
		}
	}
}
