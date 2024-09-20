namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class Product_ImageResponse
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public required string Name { get; set; }

        public Guid ProductId { get; set; }
    }
}
