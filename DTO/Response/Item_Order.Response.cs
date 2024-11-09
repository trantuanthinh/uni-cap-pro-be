using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Item_OrderResponse
    {
        public Guid Sub_OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsRating { get; set; }

        public Product? Product { get; set; }
        public Sub_Order? Sub_Order { get; set; }
    }
}
