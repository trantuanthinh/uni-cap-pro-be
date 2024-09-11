using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Discount_DetailService(DataContext dataContext, SharedService sharedService)
        : IDiscount_DetailService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly SharedService _sharedService = sharedService;

        public async Task<List<Discount_Detail>> GetDetailsByDiscountId(Guid discountId)
        {
            var details = await _dataContext
                .Discount_Details.Where(item => item.DiscountId == discountId)
                .ToListAsync();
            return details;
        }

        public async Task<ICollection<Discount_Detail>> GetDiscount_Details()
        {
            var _items = await _dataContext
                .Discount_Details.OrderBy(item => item.DiscountId)
                .ToListAsync();
            return _items;
        }

        public async Task<Discount_Detail> GetDiscount_Detail(Guid id)
        {
            var _item = await _dataContext.Discount_Details.SingleOrDefaultAsync(item =>
                item.Id == id
            );
            return _item;
        }

        public async Task<bool> CreateDiscount_Detail(Discount_Detail _item)
        {
            _item.Created_At = DateTime.UtcNow;
            _item.Modified_At = DateTime.UtcNow;
            _dataContext.Discount_Details.Add(_item);
            return Save();
        }

        public async Task<bool> UpdateDiscount_Detail(
            Discount_Detail _item,
            Discount_Detail patchItem
        )
        {
            _item.Modified_At = DateTime.UtcNow;
            _dataContext.Discount_Details.Update(_item);
            return Save();
        }

        public async Task<bool> DeleteDiscount_Detail(Discount_Detail _item)
        {
            _dataContext.Discount_Details.Remove(_item);
            return Save();
        }

        private bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}
