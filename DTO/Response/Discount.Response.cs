using System.ComponentModel.DataAnnotations;
using Core.Data.Base.Entity;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Response
{
	public class DiscountResponse: BaseEntity<Guid>
	{
		public int Level { get; set; }

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required double Amount { get; set; }
		public ActiveStatus ActiveStatus { get; set; }
	}
}
