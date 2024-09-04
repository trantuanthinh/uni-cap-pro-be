using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	public class Discount_Detail
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public required Guid DiscountId { get; set; }
		public int Level { get; set; }
		public double Amount { get; set; }
	}
}
