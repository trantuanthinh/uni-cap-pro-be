using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class FeedbackRequest
    {
        [Required]
        public Guid UserId { get; set; }

        public string? Content { get; set; }

        [Required]
        public required int Rating { get; set; }
    }
}
