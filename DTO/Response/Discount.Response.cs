using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class DiscountResponse
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }
        public required double Amount { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
        public required ICollection<Discount_Detail> Discount_Details { get; set; }
    }
}
