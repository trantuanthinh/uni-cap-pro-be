using System;

namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class Product_CategoryResponse
    {
        public class Product_Category
        {
            public Guid Id { get; set; }
            public DateTime Created_At { get; set; }
            public DateTime Modified_At { get; set; }
            public required string Name { get; set; }
        }
    }
}
