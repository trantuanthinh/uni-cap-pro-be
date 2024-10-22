using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class ProductRequest
    {
        [Required]
        public required Guid CategoryId { get; set; }

        [Required]
        public required Guid OwnerId { get; set; } // the owner of the product

        [Required]
        public required Guid DiscountId { get; set; }

        [Required]
        public required Guid UnitMeasureId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required double Price { get; set; }

        [Required]
        public required double Quantity { get; set; }
        public string? Description { get; set; }

        public ActiveStatus? Active_Status { get; set; } = ActiveStatus.ACTIVE;
    }
}
