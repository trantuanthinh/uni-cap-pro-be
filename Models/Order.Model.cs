using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Models.Setting_Data_Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Order : BaseEntity<Guid>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Order, OrderResponse>()
                .ForMember(
                    d => d.TimeLeft,
                    opt => opt.MapFrom(src => src.EndTime - DateTime.UtcNow)
                )
                .ForMember(
                    d => d.Is_Remained,
                    opt => opt.MapFrom(src => src.EndTime > DateTime.UtcNow)
                );
        });

        static readonly IMapper mapper = new Mapper(config);

        public OrderResponse ToResponse()
        {
            var res = mapper.Map<OrderResponse>(this);
            return res;
        }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string DistrictId { get; set; }

        [Required]
        public required double Total_Price { get; set; }

        public DateTime EndTime { get; set; }
        public DeliveryStatus Delivery_Status { get; set; }
        public int Level { get; set; } //number of people joined together

        [Required]
        public required bool IsShare { get; set; }
        public required bool IsActive { get; set; }

        public District? District { get; set; }
        public List<Sub_Order>? Sub_Orders { get; set; }
    }
}
