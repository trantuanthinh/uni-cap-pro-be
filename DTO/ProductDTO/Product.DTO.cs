using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.ProductDTO
{
	public class ProductDTO
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public required double Price { get; set; }
		public string? Description { get; set; }

		[Required]
		public required ActiveStatus Active_Status { get; set; }

		[Required]
		public required int Total_Rating_Value { get; set; } // the total number of stars which is rated by user

		[Required]
		public required int Total_Rating_Quantity { get; set; } // the total number of user who rated the product

		public required User Owner { get; set; } // the owner of the product
		public string? Category { get; set; }
		public Discount? Discount { get; set; }
		public List<string>? Images { get; set; }
	}
}
