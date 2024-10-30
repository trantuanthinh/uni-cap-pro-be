using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class FeedbackRequest
    {
        [Required]
        public required Guid Sub_OrderId { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        public string? Content { get; set; }

        [Required]
        public required int Rating { get; set; }
    }
}
