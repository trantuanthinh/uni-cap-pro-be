using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.ProductDTO
{
    public class Product_CategoryCreateDTO
    {
        [Required]
        public required string Name { get; set; }
    }
}
