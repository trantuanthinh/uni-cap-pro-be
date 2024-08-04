using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IUserService
	{
		User GetUserTrimToUpper(UserDTO userCreate);
		string HashPassword(string password);
		bool VerifyPassword(string password, string hashedPassword);
	}
}
