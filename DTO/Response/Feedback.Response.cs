using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class FeedbackResponse
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public UserResponse? User { get; set; }
        public ProductResponse? Product { get; set; }
    }
}
