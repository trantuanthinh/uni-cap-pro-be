using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class Product_CategoryRequest
    {
        [Required]
        public required string Name { get; set; }
    }
}
