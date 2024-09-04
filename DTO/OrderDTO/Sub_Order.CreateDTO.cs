namespace uni_cap_pro_be.DTO.OrderDTO
{
	// TODO
	public class Sub_OrderCreateDTO
	{
		public Guid ProductId { get; set; }
		public Guid UserId { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
