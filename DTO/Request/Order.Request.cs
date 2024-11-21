using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class OrderRequest
    {
        [Required]
        public List<ItemRequest> ItemRequests { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string DistrictId { get; set; }

        [Required]
        public double Total_Price { get; set; }

        [Required]
        public bool IsShare { get; set; }
    }

    public class ItemRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class BuyTogetherRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
