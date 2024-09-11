using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
	public class Discount
	{
		[Key]
		public Guid Id { get; set; }

		public ActiveStatus ActiveStatus { get; set; }

		public List<Discount_Detail>? Discount_Details { get; set; }
	}
}
