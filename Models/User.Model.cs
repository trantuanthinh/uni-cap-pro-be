using System.ComponentModel.DataAnnotations;

namespace uni_cap_pro_be.Models
{
	public class User
	{
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
		public required int PhoneNumber { get; set; }

		[Required]
		public required string Active_Status { get; set; }

		[Required]
		public required string User_Type { get; set; }

		public string? Avatar { get; set; }
		public string? Description { get; set; }

		public User() { }
	}
}
