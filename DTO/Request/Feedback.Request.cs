using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class FeedbackRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public required string Content { get; set; }
        public int? Rating { get; set; }
    }
}
