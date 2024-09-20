using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Discount_Detail : BaseEntity<Guid>
    {
        // Mapping from Discount_Detail to Discount_DetailResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Discount_Detail, Discount_DetailResponse>()
        );

        static readonly IMapper mapper = config.CreateMapper();

        public Discount_DetailResponse ToResponse()
        {
            var res = mapper.Map<Discount_DetailResponse>(this);
            return res;
        }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required Guid DiscountId { get; set; }
        public int Level { get; set; } // Level of discount ladder (1, 2, 3, 4, 5)
        public double Amount { get; set; }
    }
}
