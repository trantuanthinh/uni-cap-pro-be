using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Sub_OrderService(DataContext dataContext, IProductService productService)
        : ISub_OrderService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IProductService _productService = productService;

        public async Task<List<Sub_Order>> GetSubOrdersById(Guid OrderId)
        {
            var subOrders = await _dataContext
                .Sub_Orders.Where(item => item.OrderId == OrderId)
                .ToListAsync();
            //foreach (var item in subOrders)
            //{
            //	Product product = await _productService.GetItem(item.ProductId);
            //	item.Product = product;
            //}
            return subOrders;
        }

        public Task<ICollection<Sub_Order>> GetSub_Orders()
        {
            throw new NotImplementedException();
        }

        public async Task<Sub_Order> GetSub_Order(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateSub_Order(Sub_Order _item)
        {
            _item.Created_At = DateTime.UtcNow;
            _item.Modified_At = DateTime.UtcNow;
            _dataContext.Sub_Orders.Add(_item);
            return Save();
        }

        public async Task<bool> UpdateSub_Order(Sub_Order _item, Sub_Order patchItem)
        {
            _item.Modified_At = DateTime.UtcNow;
            _dataContext.Sub_Orders.Update(_item);
            return Save();
        }

        public async Task<bool> DeleteSub_Order(Sub_Order _item)
        {
            _dataContext.Sub_Orders.Remove(_item);
            return Save();
        }

        private bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}
