using AutoMapper;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.AutoMapperHelp
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            // ----- CreateMap<A,B> => Map from A to B

            CreateMap<UserRequest, User>();

            CreateMap<ProductRequest, Product>();

            CreateMap<BuyTogetherRequest, Sub_Order>();

            CreateMap<FeedbackRequest, Feedback>();

            CreateMap<OrderRequest, Sub_Order>();
        }
    }
}
