using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Discount_DetailResponse
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }
        public required Guid DiscountId { get; set; }
        public int Level { get; set; }
        public double Amount { get; set; }
    }
}
