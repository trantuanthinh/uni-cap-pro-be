using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.DiscountDTO
{
	// DONE
	public class DiscountCreateDTO
	{
		[Required]
		public required short Level { get; set; }
		[Required]
		public required float Amount { get; set; }
	}
}
