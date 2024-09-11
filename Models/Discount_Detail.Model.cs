using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
    public class Discount_Detail
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required Guid DiscountId { get; set; }
        public int Level { get; set; }
        public double Amount { get; set; }
    }
}
