using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class OrderRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool IsShare { get; set; }
    }
}
