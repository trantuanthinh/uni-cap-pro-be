using AutoMapper;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.AutoMapperHelp
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            //CreateMap<A,B> => Map from A to B

            // CreateMap<User, UserDTO>();
            CreateMap<UserRequest, User>();

            //CreateMap<Product, ProductDTO>()
            //	.ForMember(d => d.Category, opt => opt.MapFrom(src => src.Category.Name));
            //CreateMap<ProductDTO, Product>();

            //CreateMap<Product_Image, Product_ImageDTO>();
            //CreateMap<Product_ImageDTO, Product_Image>();

            //CreateMap<Order, OrderDTO>();
            CreateMap<BuyTogetherRequest, Sub_Order>();

            CreateMap<OrderRequest, Sub_Order>();

            //CreateMap<Discount_DetailDTO, Discount_Detail>()
            //	.ForMember(d => d.DiscountId, opt => opt.Ignore());
        }
    }
}
