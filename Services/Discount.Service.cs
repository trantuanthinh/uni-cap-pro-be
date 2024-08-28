using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	// TODO
	public class DiscountService<T> : IDiscountService<T> where T : Discount
	{
		private readonly DataContext _dataContext;
		private readonly DbSet<T> _dataSet;
		private readonly SharedService _sharedService;
		private readonly IDiscount_DetailService<Discount_Detail> _detailService;

		public DiscountService(DataContext dataContext, SharedService sharedService, IDiscount_DetailService<Discount_Detail> detailService)
		{
			_dataContext = dataContext;
			_dataSet = _dataContext.Set<T>();
			_sharedService = sharedService;
			_detailService = detailService;
		}

		public async Task<ICollection<T>> GetItems()
		{
			var _items = await _dataSet.OrderBy(item => item.Id).ToListAsync();
			foreach (var item in _items)
			{
				ICollection<Discount_Detail> details = await _detailService.GetDetailsById(item.Id);
				item.Discount_Details = details;
			}
			return _items;
		}

		public async Task<T> GetItem(Guid id)
		{
			T _item = await _dataSet.SingleOrDefaultAsync(item => item.Id == id);
			ICollection<Discount_Detail> details = await _detailService.GetDetailsById(_item.Id);
			_item.Discount_Details = details;
			return _item;
		}

		public async Task<bool> CreateItem(T _item)
		{
			_item.Created_At = DateTime.UtcNow;
			_item.Modified_At = DateTime.UtcNow;
			_dataSet.Add(_item);
			return Save();
		}

		public async Task<bool> UpdateItem(T _item, T patchItem)
		{
			_item.Modified_At = DateTime.UtcNow;
			_dataSet.Update(_item);
			return Save();
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
