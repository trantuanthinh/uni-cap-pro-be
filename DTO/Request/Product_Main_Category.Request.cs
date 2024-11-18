using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class Product_Main_CategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
