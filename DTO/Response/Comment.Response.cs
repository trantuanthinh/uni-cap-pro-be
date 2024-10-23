namespace uni_cap_pro_be.Models
{
    // DONE
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
