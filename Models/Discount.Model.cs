using System.Text.Json.Serialization;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Discount : BaseEntity<Guid>
    {
        // Mapping from Discount to DiscountResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Discount, DiscountResponse>();
        });

        static readonly IMapper mapper = config.CreateMapper();

        public DiscountResponse ToResponse()
        {
            var res = mapper.Map<DiscountResponse>(this);
            return res;
        }

        public string Name { get; set; }
        public ActiveStatus Active_Status { get; set; }

        public ICollection<Discount_Detail>? Discount_Details { get; set; }
    }
}
