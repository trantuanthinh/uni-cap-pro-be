using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Exceptions;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class AuthService(AuthRepository repository, SharedService sharedService) : BaseService()
    {
        public readonly AuthRepository _repository = repository;
        public readonly SharedService _sharedService = sharedService;

        public async Task<bool> CreateUser(User _item)
        {
            bool validUser = await IsUserUniqueAsync(_item);
            if (!validUser)
            {
                return false;
            }
            _item.Password = _sharedService.HashPassword(_item.Password);
            _repository.Add(_item);
            return _repository.Save();
        }

        public User AuthenticatedUser(SignInRequest item)
        {
            User _user =
                GetUserByUsernameType(item.Username)
                ?? throw new NotFoundException("User not found");

            bool verifyPassword = _sharedService.VerifyPassword(item.Password, _user.Password);

            if (!verifyPassword)
            {
                return null;
            }

            return _user;
        }

        public async Task<bool> IsUserUniqueAsync(User _item)
        {
            string trimmedUpperUsername = _item.Username.Trim().ToUpperInvariant();
            string trimmedUpperEmail = _item.Email.Trim().ToUpperInvariant();

            bool isUnique = !await _repository
                .SelectAll()
                .AnyAsync(user =>
                    user.Username.Trim().ToUpper() == trimmedUpperUsername // Case-insensitive comparison
                    || user.Email.Trim().ToUpper() == trimmedUpperEmail // Case-insensitive comparison
                    || user.PhoneNumber == _item.PhoneNumber // Phone number comparison
                );
            return isUnique;
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
            User _user = _repository
                .SelectAll()
                .Where(item => item.Email == email)
                .FirstOrDefault();
            return _user;
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            User _user = _repository
                .SelectAll()
                .Where(item => item.PhoneNumber == phoneNumber)
                .FirstOrDefault();
            return _user;
        }

        public User GetUserByUserName(string username)
        {
            User _user = _repository
                .SelectAll()
                .Where(item => item.Username == username)
                .FirstOrDefault();
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
