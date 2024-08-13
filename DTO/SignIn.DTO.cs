using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.DTO
{
    public class SignInDTO
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
