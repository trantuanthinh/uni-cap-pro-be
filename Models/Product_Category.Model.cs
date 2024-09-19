using System.ComponentModel.DataAnnotations;
using Core.Data.Base.Entity;

namespace uni_cap_pro_be.Models
{
    public class Product_Category : BaseEntity<Guid>
    {
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
