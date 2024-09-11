using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.OrderDTO
{
	public class OrderCreateDTO
	{
		[Required]
		public required Guid ProductId { get; set; }

		[Required]
		public required Guid UserId { get; set; }

		[Required]
		public required int Quantity { get; set; }

		[Required]
		public required double Price { get; set; }

		[Required]
		public required bool IsShare { get; set; }
	}
}
