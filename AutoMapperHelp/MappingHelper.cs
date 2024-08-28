using AutoMapper;
using uni_cap_pro_be.DTO.DiscountDTO;
using uni_cap_pro_be.DTO.OrderDTO;
using uni_cap_pro_be.DTO.ProductDTO;
using uni_cap_pro_be.DTO.UserDTO;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.AutoMapperHelp
{
	public class MappingHelper : Profile
	{
		public MappingHelper()
		{
			//CreateMap<A,B> => Map from A to B

			CreateMap<User, UserDTO>();
			CreateMap<UserDTO, User>();

			CreateMap<Product, ProductDTO>();
			CreateMap<ProductDTO, Product>();

			//CreateMap<Product_Category, Product_CategoryDTO>();
			//CreateMap<Product_CategoryDTO, Product_Category>();

			CreateMap<Product_Image, Product_ImageDTO>().ForMember(dest => dest.OwnerId, opt => opt.Ignore());
			CreateMap<Product_ImageDTO, Product_Image>();

			CreateMap<Order, OrderDTO>();
			CreateMap<OrderDTO, Order>();

			CreateMap<Discount_DetailDTO, Discount_Detail>()
				.ForMember(dest => dest.DiscountId, opt => opt.Ignore());
		}
	}
}
