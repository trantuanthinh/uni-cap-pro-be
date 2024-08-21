using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.ProductDTO
{
	// DONE
	public class Product_ImageDTO
	{
		[Required]
		public Guid OwnerId { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public Guid ProductId { get; set; }
	}
}
