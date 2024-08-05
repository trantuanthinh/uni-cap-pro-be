using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	public class UserService(DataContext dataContext, SharedService sharedService) : IUserService
	{
		public readonly DataContext _dataContext = dataContext;
		public readonly SharedService _sharedService = sharedService;

		public ICollection<User> GetUsers()
		{
			return _dataContext.Users.OrderBy(item => item.Id).ToList();
		}

		public User GetUser(Guid id)
		{
			User _item = _dataContext.Users.SingleOrDefault(user => user.Id == id);
			return _item;
		}

		public bool CreateUser(User _item)
		{
			bool validUser = CheckValidUser(_item);
			if (!validUser)
			{
				return false;
			}
			_item.Password = _sharedService.HashPassword(_item.Password);
			_item.Created_At = DateTime.UtcNow;
			_item.Modified_At = DateTime.UtcNow;
			_dataContext.Users.Add(_item);
			return Save();
		}

		public bool UpdateUser(User _item, User patchItem)
		{
			_item.Modified_At = DateTime.UtcNow;
			_item.Username = patchItem.Username;
			_item.Name = patchItem.Name;
			_item.Email = patchItem.Email;
			if (patchItem.Password != null)
			{
				patchItem.Password = _sharedService.HashPassword(patchItem.Password);
				_item.Password = patchItem.Password;
			}
			_item.PhoneNumber = patchItem.PhoneNumber;
			_item.Active_Status = patchItem.Active_Status;
			_item.User_Type = patchItem.User_Type;
			_item.Description = patchItem.Description;
			_item.Avatar = patchItem.Avatar;
			_dataContext.Users.Update(_item);
			return Save();
		}

		public bool DeleteUser(User _item)
		{
			_dataContext.Users.Remove(_item);
			return Save();
		}

		public bool CheckValidUser(User userCreate)
		{
			var trimmedUpperUsername = userCreate.Username.Trim().ToUpper();
			var trimmedUpperEmail = userCreate.Email.Trim().ToUpper();
			var phoneNumber = userCreate.PhoneNumber;

			return !GetUsers().Any(user =>
				user.Username.Trim().Equals(trimmedUpperUsername, StringComparison.CurrentCultureIgnoreCase) ||
				user.Email.Trim().Equals(trimmedUpperEmail, StringComparison.CurrentCultureIgnoreCase) ||
				user.PhoneNumber == phoneNumber);
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
