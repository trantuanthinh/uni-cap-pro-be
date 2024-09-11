using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class UserService(DataContext dataContext, SharedService sharedService) : IUserService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly SharedService _sharedService = sharedService;

        public async Task<ICollection<User>> GetUsers()
        {
            var _items = await _dataContext.Users.OrderBy(item => item.Id).ToListAsync();
            return _items;
        }

        public async Task<User> GetUser(Guid id)
        {
            var _item = await _dataContext.Users.SingleOrDefaultAsync(item => item.Id == id);
            return _item;
        }

        public async Task<bool> CreateUser(User _item)
        {
            bool validUser = await IsUserUniqueAsync(_item);
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

        public async Task<bool> UpdateUser(User _item, User patchItem)
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

        public async Task<bool> DeleteUser(User _item)
        {
            _dataContext.Users.Remove(_item);
            return Save();
        }

        public async Task<bool> IsUserUniqueAsync(User _item)
        {
            string trimmedUpperUsername = _item.Username.Trim().ToUpperInvariant();
            string trimmedUpperEmail = _item.Email.Trim().ToUpperInvariant();

            bool isUnique = !await _dataContext.Users.AnyAsync(user =>
                user.Username.Trim()
                    .Equals(trimmedUpperUsername, StringComparison.CurrentCultureIgnoreCase)
                || user.Email.Trim()
                    .Equals(trimmedUpperEmail, StringComparison.CurrentCultureIgnoreCase)
                || user.PhoneNumber == _item.PhoneNumber
            );
            return isUnique;
        }

        private bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}
