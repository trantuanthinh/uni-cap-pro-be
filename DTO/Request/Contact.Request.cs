using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using CsvHelper.Configuration.Attributes;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class ContactRequest
    {
        [Required]
        [DefaultValue("conan246817@gmail.com")]
        public required string Email { get; set; }

        [Required]
        public required string Subject { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public required string Message { get; set; }
    }
}
