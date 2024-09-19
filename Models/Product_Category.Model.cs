using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Product_Category : BaseEntity<Guid>
    {
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
