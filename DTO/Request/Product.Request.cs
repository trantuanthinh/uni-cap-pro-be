using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
	public class ProductCreateDTO
	{
		[Required]
		public required Guid CategoryId { get; set; }

		[Required]
		public required Guid OwnerId { get; set; } // the owner of the product

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required string Name { get; set; }

		[Required]
		public required double Price { get; set; }
		public string? Description { get; set; }

		[Required]
		public required int Total_Rating_Value { get; set; } // the total number of stars which is rated by user

		[Required]
		public required int Total_Rating_Quantity { get; set; } // the total number of user who rated the product
	}
}
