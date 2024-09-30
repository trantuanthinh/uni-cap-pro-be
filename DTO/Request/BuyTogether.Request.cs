using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class BuyTogetherRequest
    {
        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required double Price { get; set; }
    }
}
