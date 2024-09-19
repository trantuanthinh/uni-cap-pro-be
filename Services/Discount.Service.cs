// using Microsoft.EntityFrameworkCore;
// using uni_cap_pro_be.Data;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Repositories;

// namespace uni_cap_pro_be.Services
// {
// 	// DONE
// 	public class DiscountService(DataContext dataContext, IDiscount_DetailService detailService)
// 		: IDiscountService
// 	{
// 		private readonly DataContext _dataContext = dataContext;
// 		private readonly IDiscount_DetailService _detailService = detailService;

// 		public async Task<ICollection<Discount>> GetDiscounts()
// 		{
// 			var _items = await _dataContext.Discounts.OrderBy(item => item.Id).ToListAsync();
// 			foreach (var item in _items)
// 			{
// 				List<Discount_Detail> details = await _detailService.GetDetailsByDiscountId(
// 					item.Id
// 				);
// 				item.Discount_Details = details;
// 			}
// 			return _items;
// 		}

// 		public async Task<Discount> GetDiscount(Guid id)
// 		{
// 			Discount _item = await _dataContext.Discounts.SingleOrDefaultAsync(item =>
// 				item.Id == id
// 			);
// 			List<Discount_Detail> details = await _detailService.GetDetailsByDiscountId(_item.Id);
// 			_item.Discount_Details = details;
// 			return _item;
// 		}

// 		public async Task<bool> CreateDiscount(Discount _item)
// 		{
// 			_dataContext.Discounts.Add(_item);
// 			return Save();
// 		}

// 		public async Task<bool> UpdateDiscount(Discount _item, Discount patchDiscount)
// 		{
// 			_dataContext.Discounts.Update(_item);
// 			return Save();
// 		}

// 		public async Task<bool> DeleteDiscount(Discount _item)
// 		{
// 			_dataContext.Discounts.Remove(_item);
// 			return Save();
// 		}

// 		private bool Save()
// 		{
// 			int saved = _dataContext.SaveChanges();
// 			return saved > 0;
// 		}
// 	}
// }
