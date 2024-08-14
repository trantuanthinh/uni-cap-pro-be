using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO
{
	// DONE
	public class Product_CategoryDTO
	{
		[Required]
		public required string Name { get; set; }
	}
}
