using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	// DONE
	public class Sub_Order
	{
		[Key]
		public Guid Id { get; set; }

		public Guid UserId { get; set; }
		public Guid OrderId { get; set; }

		public required User User { get; set; }
		public required Order Order { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
