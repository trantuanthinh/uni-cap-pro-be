using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class OTPRequest
    {
        [Required]
        [DefaultValue("conan246817@gmail.com")]
        public required string Email { get; set; }
    }

    public class OTPVerifyRequest
    {
        [Required]
        [DefaultValue("conan246817@gmail.com")]
        public required string Email { get; set; }

        [Required]
        public required string OTP { get; set; }
    }
}
