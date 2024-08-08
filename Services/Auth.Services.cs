using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	public class AuthService(DataContext dataContext, SharedService sharedService) : IAuthService
	{
		public readonly DataContext _dataContext = dataContext;
		public readonly SharedService _sharedService = sharedService;

		public User AuthenticatedUser(SignInDTO item)
		{
			User _user = GetUserByUsernameType(item.Username);

			if (_user == null)
			{
				return null;
			}

			bool verifyPassword = _sharedService.VerifyPassword(item.Password, _user.Password);

			if (!verifyPassword)
			{
				return null;
			}

			return _user;
		}

		public User GetUserByUsernameType(string username)
		{
			var usernameType = ChooseUsernameType(username);

			return usernameType switch
			{
				UsernameType.Email => GetUserByEmail(username),
				UsernameType.PhoneNumber => GetUserByPhoneNumber(username),
				UsernameType.UserName => GetUserByUserName(username),
				_ => throw new ArgumentException("Invalid username type"),
			};
		}

		public User GetUserByEmail(string email)
		{
			User _user = _dataContext.Users.Where(item => item.Email == email).FirstOrDefault();
			return _user;
		}

		public User GetUserByPhoneNumber(string phoneNumber)
		{
			User _user = _dataContext.Users.Where(item => item.PhoneNumber == phoneNumber).FirstOrDefault();
			return _user;
		}

		public User GetUserByUserName(string username)
		{
			User _user = _dataContext.Users.Where(item => item.Username == username).FirstOrDefault();
			return _user;
		}

		public UsernameType ChooseUsernameType(string username)
		{
			if (_sharedService.IsValidGmail(username))
			{
				return UsernameType.Email;
			}

			if (_sharedService.IsNumber(username))
			{
				return UsernameType.PhoneNumber;
			}

			return UsernameType.UserName;
		}
	}
}
