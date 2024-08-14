using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
	public class Order
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }
		public required Guid ProductId { get; set; }

		[Required]
		public required double Total_Price { get; set; }

		[Required]
		public required int Total_Quantity { get; set; }

		[Required]
		public required int Bundle { get; set; }

		public DateTime Timer { get; set; }
		public DateTime Remaining_Timer { get; set; }
		public bool Is_Remained { get; set; }
		public int Level { get; set; } //number of people joined together

		[Required]
		public DeliveryStatus Delivery_Status { get; set; }



		public required Product Product { get; set; }
		public required ICollection<Sub_Order> Sub_Orders { get; set; }
	}
}
