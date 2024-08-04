using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public User GetUserTrimToUpper(UserDTO userCreate)
		{
			return _userRepository.GetUsers().FirstOrDefault(
				u => u.Username.Trim().ToUpper() == userCreate.Username.Trim().ToUpper() ||
				u.Email.Trim().ToUpper() == userCreate.Email.Trim().ToUpper() ||
				u.PhoneNumber == userCreate.PhoneNumber);
		}

		public string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
	}
}
