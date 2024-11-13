using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class ContactRequest
    {
        [Required]
        [DefaultValue("tran.tuan.thinh.0125@gmail.com")]
        public required string Email { get; set; }

        [Required]
        public required string Subject { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public required string Message { get; set; }
    }
}
