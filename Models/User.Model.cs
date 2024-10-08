using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class User : BaseEntity<Guid>
    {
        // Mapping from User to UserResponse
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<User, UserResponse>()
        );

        static readonly IMapper mapper = config.CreateMapper();

        public UserResponse ToResponse()
        {
            var res = mapper.Map<UserResponse>(this);
            return res;
        }

        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required ActiveStatus Active_Status { get; set; }

        [Required]
        public required UserType Type { get; set; }

        public string? Avatar { get; set; }
        public string? Background { get; set; }
        public string? Description { get; set; }

        public ICollection<Product>? Products { get; set; } // for user type producer
        // public ICollection<Sub_Order>? Sub_Orders { get; set; } // for user type company
    }
}
