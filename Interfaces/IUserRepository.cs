using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IUserRepository
	{
		ICollection<User> GetUsers();
		User GetUser(Guid id);
		bool CreateUser(User user);
	}
}
