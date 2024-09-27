using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class ProductRequest
    {
        [Required]
        public required Guid CategoryId { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required double Price { get; set; }
        public string? Description { get; set; }

        [Required]
        public required int Total_Rating_Value { get; set; } // the total number of stars which is rated by user

        [Required]
        public required int Total_Rating_Quantity { get; set; } // the total number of user who rated the product
    }
}
