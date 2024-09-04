namespace uni_cap_pro_be.DTO.OrderDTO
{
	// TODO
	public class Sub_OrderCreateDTO
	{
		public Guid UserId { get; set; }
		public Guid OrderId { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
