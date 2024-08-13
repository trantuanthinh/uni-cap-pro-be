using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
    public class Order_Detail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        [Required]
        public required Order Order { get; set; }

        [Required]
        public required Product Product { get; set; }

        [Required]
        public required User User { get; set; }
    }
}
