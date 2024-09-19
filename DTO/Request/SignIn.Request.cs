using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO.Request
{
    public class SignInRequest
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
