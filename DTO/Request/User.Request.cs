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
        public required string Address { get; set; }

        [Required]
        public required string ProvinceId { get; set; }

        [Required]
        public required string DistrictId { get; set; }

        [Required]
        public required string WardId { get; set; }

        public UserType User_Type { get; set; } = UserType.BUYER;
        public string? Avatar { get; set; } = null;
        public string? Background { get; set; } = null;
        public string? Description { get; set; } = null;
    }
}
