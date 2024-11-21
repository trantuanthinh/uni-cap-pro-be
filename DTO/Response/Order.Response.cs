using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }
        public string DistrictId { get; set; }
        public string District { get; set; }
        public double Total_Price { get; set; }
        public Guid StoreId { get; set; }
        public UserResponse Store { get; set; }
        public DateTime EndTime { get; set; }
        public DeliveryStatus Delivery_Status { get; set; }
        public int Level { get; set; }
        public bool IsShare { get; set; }
        public bool IsActive { get; set; }
        public List<Sub_Order>? Sub_Orders { get; set; }

        public TimeSpan TimeLeft { get; set; }
        public bool Is_Remained { get; set; }
    }
}
