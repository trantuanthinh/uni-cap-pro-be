using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class UserSub_OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public required Guid UserId { get; set; }
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public Product Product { get; set; }

        // public required User User { get; set; }
    }
}
