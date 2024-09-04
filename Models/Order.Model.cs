using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
	// DONE
	public class Order
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required decimal Total_Price { get; set; }

		[Required]
		public required int Total_Quantity { get; set; }

		public TimeSpan Timer { get; set; }
		public DateTime Remaining_Timer { get; set; }
		public bool Is_Remained { get; set; }
		public int Level { get; set; } //number of people joined together
		public DeliveryStatus Delivery_Status { get; set; }



		public List<Sub_Order>? Sub_Orders { get; set; }
	}
}
