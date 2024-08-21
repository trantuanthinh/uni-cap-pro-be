using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.OrderDTO
{
	// TODO
	public class Sub_OrderCreateDTO
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		public Guid UserId { get; set; }
		public Guid OrderId { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
