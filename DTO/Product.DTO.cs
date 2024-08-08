using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO
{
	public class ProductDTO
	{
		[Required]
		public Guid CategoryId { get; set; }
		[Required]
		public Guid OwnerId { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public required double Price { get; set; }

		[Required]
		public required ActiveStatus Active_Status { get; set; }

		public int Total_Rating_Value { get; set; } // the total number of stars which is rated by user
		public int Total_Rating_Quantity { get; set; } // the total number of user who rated the product
	}
}
