using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Request
{
    // DONE
    public class UserRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DefaultValue("string@gmail.com")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DefaultValue("0327858682")]
        public string PhoneNumber { get; set; }

        [Required]
        public UserType User_Type { get; set; }
        public string? Avatar { get; set; }
        public string? Background { get; set; }
        public string? Description { get; set; }
    }
}
