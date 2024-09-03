using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.OrderDTO
{
	// DONE
	public class OrderCreateDTO
	{
		public Guid OwnerId { get; set; }

		[Required]
		public required double Total_Price { get; set; }

		public DeliveryStatus Delivery_Status { get; set; }
	}
}
