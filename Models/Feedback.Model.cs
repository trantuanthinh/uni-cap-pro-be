using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Feedback : BaseEntity<Guid>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Feedback, FeedbackResponse>()
                .ForMember(d => d.User, opt => opt.MapFrom(src => src.Sub_Order.User.ToResponse()))
                .ForMember(d => d.Product, opt => opt.MapFrom(src => src.Product.ToResponse()));
        });

        static readonly IMapper mapper = config.CreateMapper();

        public FeedbackResponse ToResponse()
        {
            var res = mapper.Map<FeedbackResponse>(this);
            return res;
        }

        [Required]
        public required Guid Sub_OrderId { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        public required int Rating { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public string? Video { get; set; }

        public Sub_Order? Sub_Order { get; set; }
        public Product? Product { get; set; }
    }
}
