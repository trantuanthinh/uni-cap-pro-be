using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class ProductRequest
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid OwnerId { get; set; } // the owner of the product

        [Required]
        public Guid DiscountId { get; set; }

        [Required]
        public Guid UnitMeasureId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Quantity { get; set; }
        public string? Description { get; set; }

        public ActiveStatus? Active_Status { get; set; }
    }
}
