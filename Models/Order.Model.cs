using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
	public class Order
	{
		[Key]
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }

		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		[Required]
		public required double Total_Price { get; set; }

		[Required]
		public required int Bundle { get; set; }

		public DateTime Timer { get; set; }
		public DateTime Remaining_Timer { get; set; }
		public bool Is_Remained { get; set; }

		[Required]
		public DeliveryStatus Delivery_Status { get; set; }

		public required ICollection<Product> Products { get; set; }
		public required ICollection<User> Owners { get; set; }
	}
}
