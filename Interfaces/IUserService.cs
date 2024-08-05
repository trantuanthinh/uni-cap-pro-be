using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IUserService
	{
		ICollection<User> GetUsers();
		User GetUser(Guid id);
		bool CreateUser(User item);
		bool UpdateUser(User _item, User patchItem);
		bool DeleteUser(User item);
		bool CheckValidUser(User user); //check whether if user is duplicated - email, phone number, username
		bool Save();
	}
}
