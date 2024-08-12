using Microsoft.EntityFrameworkCore;
using System.Data;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	public class OrderService<T> : IBaseService<T> where T : Order
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

		public ICollection<T> GetItems()
		{
			return _dataSet.OrderBy(item => item.Id).ToList();
		}

		public T GetItem(Guid id)
		{
			T _item = _dataSet.SingleOrDefault(item => item.Id == id);
			return _item;
		}

		public bool CreateItem(T item)
		{
			throw new NotImplementedException();
		}

		public bool UpdateItem(T item, T patchItem)
		{
			throw new NotImplementedException();
		}

		public bool DeleteItem(T item)
		{
			throw new NotImplementedException();
		}
		public bool Save()
		{
			// Implement logic to save changes to the database
			throw new NotImplementedException();
		}
	}
}
