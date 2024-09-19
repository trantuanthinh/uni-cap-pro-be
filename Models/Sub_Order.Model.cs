using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Sub_Order : BaseEntity<Guid>
    {
        // Mapping from User to UserResponse
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
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        // public required User User { get; set; }
    }
}
