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
        public ActiveStatus ActiveStatus { get; set; }
        public string Name { get; set; }
        public required ICollection<Discount_Detail> Discount_Details { get; set; }
    }
}
