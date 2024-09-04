namespace uni_cap_pro_be.DTO.OrderDTO
{
	// DONE
	public class OrderCreateDTO
	{
		public Guid ProductId { get; set; }
		public Guid UserId { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
