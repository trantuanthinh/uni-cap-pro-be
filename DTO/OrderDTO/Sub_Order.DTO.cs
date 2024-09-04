using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.DTO.OrderDTO
{
	// DONE
	public class Sub_OrderDTO
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		public required User User { get; set; }
		public required Order Order { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
