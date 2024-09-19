using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Sub_Order : BaseEntity<Guid>
    {
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required Guid UserId { get; set; }
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        // public required User User { get; set; }
    }
}
