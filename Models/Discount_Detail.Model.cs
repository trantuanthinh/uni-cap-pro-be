using Core.Data.Base.Entity;
using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	public class Discount_Detail : BaseEntity<Guid>
	{
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required Guid DiscountId { get; set; }
		public int Level { get; set; }
		public double Amount { get; set; }
	}
}
