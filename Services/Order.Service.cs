using Microsoft.EntityFrameworkCore;
using System.Data;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	// TODO
	public class OrderService<T> : IOrderService<T> where T : Order
	{
		private readonly DataContext _dataContext;
		private readonly DbSet<T> _dataSet;
		private readonly SharedService _sharedService;

		public OrderService(DataContext dataContext, SharedService sharedService)
		{
			_dataContext = dataContext;
			_dataSet = _dataContext.Set<T>();
			_sharedService = sharedService;
		}

		public async Task<ICollection<T>> GetItems()
		{
			var items = await _dataSet.OrderBy(item => item.Id).ToListAsync();
			return items;
		}

		public async Task<T> GetItem(Guid id)
		{
			T _item = await _dataSet.SingleOrDefaultAsync(item => item.Id == id);
			return _item;
		}

		public async Task<bool> CreateItem(T _item)
		{
			_item.Created_At = DateTime.UtcNow;
			_item.Modified_At = DateTime.UtcNow;
			_item.Timer = new TimeSpan(1, 30, 0);
			_item.Remaining_Timer = DateTime.Now + _item.Timer;
			_item.Is_Remained = true;
			_item.Delivery_Status = DeliveryStatus.PENDING;


			//TimeSpan timeLeft = _item.Remaining_Timer - DateTime.Now;
			_dataContext.Orders.Add(_item);
			return Save();
		}

		public async Task<bool> UpdateItem(T _item, T patchItem)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteItem(T _item)
		{
			_dataSet.Remove(_item);
			return Save();
		}
		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
