using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.DiscountDTO
{
	// DONE
	public class DiscountDTO
	{
		[Key]
		public Guid Id { get; set; }
		public int Level { get; set; }

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required double Amount { get; set; }
		public ActiveStatus ActiveStatus { get; set; }
	}
}
