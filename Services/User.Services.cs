using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	// DONE
	public class UserService<T> : IUserService<T> where T : User
	{
		private readonly DataContext _dataContext;
		private readonly DbSet<T> _dataSet;
		private readonly SharedService _sharedService;

		public UserService(DataContext dataContext, SharedService sharedService)
		{
			_dataContext = dataContext;
			_dataSet = _dataContext.Set<T>();
			_sharedService = sharedService;
		}

		public async Task<ICollection<T>> GetItems()
		{
			ICollection<T> _items = await _dataSet.OrderBy(item => item.Id).ToListAsync();
			return _items;
		}

		public async Task<T> GetItem(Guid id)
		{
			T _item = await _dataSet.SingleOrDefaultAsync(item => item.Id == id);
			return _item;
		}

		public async Task<bool> CreateItem(T _item)
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

		public async Task<bool> UpdateItem(T _item, T patchItem)
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

		public async Task<bool> DeleteItem(T _item)
		{
			_dataContext.Users.Remove(_item);
			return Save();
		}

		public async Task<bool> IsUserUniqueAsync(T _item)
		{
			string trimmedUpperUsername = _item.Username.Trim().ToUpperInvariant();
			string trimmedUpperEmail = _item.Email.Trim().ToUpperInvariant();

			bool isUnique = !await _dataSet.AnyAsync(user =>
				user.Username.Trim().Equals(trimmedUpperUsername, StringComparison.CurrentCultureIgnoreCase) ||
				user.Email.Trim().Equals(trimmedUpperEmail, StringComparison.CurrentCultureIgnoreCase) ||
				user.PhoneNumber == _item.PhoneNumber);
			return isUnique;
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
