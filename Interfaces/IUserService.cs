using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	public interface IUserService<T> : IBaseService<T> where T : User
	{
		Task<bool> IsUserUniqueAsync(T user); //check whether if user is duplicated - email, phone number, username
	}
}
