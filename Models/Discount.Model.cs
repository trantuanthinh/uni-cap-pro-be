using Core.Data.Base.Entity;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
	public class Discount : BaseEntity<Guid>
	{
		public ActiveStatus ActiveStatus { get; set; }

		public List<Discount_Detail>? Discount_Details { get; set; }
	}
}
