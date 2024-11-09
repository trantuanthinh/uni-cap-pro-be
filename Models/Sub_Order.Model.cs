using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Sub_Order : BaseEntity<Guid>
    {
        // Mapping from Sub_Order to Sub_OrderResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Sub_Order, Sub_OrderResponse>()
        );

        static readonly IMapper mapper = config.CreateMapper();

        public Sub_OrderResponse ToResponse()
        {
            var res = mapper.Map<Sub_OrderResponse>(this);
            return res;
        }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required Guid OrderId { get; set; }

        [Required]
        public required bool IsPaid { get; set; }
        public double Total_Price { get; set; }

        public User? User { get; set; }
        public Order? Order { get; set; }
        public List<Item_Order>? Item_Orders { get; set; }
    }
}
