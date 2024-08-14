using System.ComponentModel.DataAnnotations;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Models
{
	// DONE
	public class User
	{
		[Key]
		public Guid Id { get; set; }
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
		public required UserType User_Type { get; set; }

		public string? Avatar { get; set; }
		public string? Background { get; set; }
		public string? Description { get; set; }

		public ICollection<Product>? Products { get; set; } // for user type producer
		public ICollection<Sub_Order>? Sub_Orders { get; set; } // for user type company
	}
}
