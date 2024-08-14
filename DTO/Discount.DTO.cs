using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO
{
	// DONE
	public class DiscountDTO
	{
		[Required]
		public required short Level { get; set; }
		[Required]
		public required double Amount { get; set; }
	}
}
