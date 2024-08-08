using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	public class ProductImage
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created_At { get; set; }

		[Required]
		public Guid ProductId { get; set; }
	}
}
