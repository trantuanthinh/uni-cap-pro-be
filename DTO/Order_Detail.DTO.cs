namespace uni_cap_pro_be.DTO
{
    public class Order_DetailDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
