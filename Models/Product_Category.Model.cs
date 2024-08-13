using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	public class Product_Category
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required string Name { get; set; }
	}
}
