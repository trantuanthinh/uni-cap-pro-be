using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class DiscountRequest
    {
        [Required]
        public short Level { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
