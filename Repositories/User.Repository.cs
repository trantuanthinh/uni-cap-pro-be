using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	public class UserRepository : IUserRepository
	{
		public readonly DataContext _dataContext;

		public UserRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public ICollection<User> GetUsers()
		{
			return _dataContext.Users.OrderBy(item => item.Id).ToList();
		}

		public User GetUser(Guid id)
		{
			var _user = _dataContext.Users.SingleOrDefault(user => user.Id == id);
			return _user;
		}

		public bool CreateUser(User user)
		{
			_dataContext.Users.Add(user);
			return Save();
		}

		public bool Save()
		{
			var saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
