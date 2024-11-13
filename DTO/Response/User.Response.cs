using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.DTO.Response
{
    // DONE
    public class UserResponse
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public UserType Type { get; set; }

        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }

        public required ActiveStatus Active_Status { get; set; }

        public string? Avatar { get; set; }
        public string? Background { get; set; }
        public string? Description { get; set; }
    }
}
