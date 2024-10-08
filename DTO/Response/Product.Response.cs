using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public required string Name { get; set; }

        public required double Price { get; set; }
        public string? Description { get; set; }

        public required ActiveStatus Active_Status { get; set; }

        public required int Total_Rating_Value { get; set; } // the total number of stars which is rated by user

        public required int Total_Rating_Quantity { get; set; } // the total number of user who rated the product

        public required string Owner { get; set; }
        public string? Category { get; set; }

        public DiscountResponse? Discount { get; set; }
        public List<string>? Images { get; set; }
    }
}
