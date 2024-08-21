using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	// TODO
	public class Discount_DetailService<T> : IDiscount_DetailService<T> where T : Discount_Detail
	{
		private readonly DataContext _dataContext;
		private readonly DbSet<T> _dataSet;
		private readonly SharedService _sharedService;

		public Discount_DetailService(DataContext dataContext, SharedService sharedService)
		{
			_dataContext = dataContext;
			_dataSet = _dataContext.Set<T>();
			_sharedService = sharedService;
		}

		public async Task<List<T>> GetDetailsById(Guid discountId)
		{
			var details = await _dataSet
				.Where(item => item.DiscountId == discountId)
				.ToListAsync();
			//.Select(item => new Discount_DetailDTO
			//{
			//	Level = item.Level,
			//	Amount = item.Amount
			//})

			return details;
		}


		public async Task<ICollection<T>> GetItems()
		{
			var items = await _dataSet.OrderBy(item => item.DiscountId).ToListAsync();
			return items;
		}

		public async Task<T> GetItem(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> CreateItem(T _item)
		{
			_dataSet.Add(_item);
			return Save();
		}

		public async Task<bool> UpdateItem(T _item, T patchItem)
		{
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
