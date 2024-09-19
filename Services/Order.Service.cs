// using System.Data;
// using Microsoft.EntityFrameworkCore;
// using uni_cap_pro_be.Core;
// using uni_cap_pro_be.Core.QueryParameter;
// using uni_cap_pro_be.Data;
// using uni_cap_pro_be.Models;
// using uni_cap_pro_be.Utils;

// namespace uni_cap_pro_be.Services
// {
//     // DONE
//     public class OrderService(DataContext dataContext)
//     {
//         private readonly DataContext _dataContext = dataContext;

//         public async Task<ICollection<Order>> GetOrders(QueryParameters queryParameters)
//         {
//             var items = await _dataContext.Orders.OrderBy(item => item.Id).ToListAsync();
//             return items;
//         }

//         public async Task<Order> GetOrder(Guid id)
//         {
//             Order _item = await _dataContext.Orders.SingleOrDefaultAsync(item => item.Id == id);
//             return _item;
//         }

//         public async Task<bool> CreateOrder(Order _item)
//         {
//             TimeSpan Timer = new(24, 0, 0);

//             _item.Created_At = DateTime.UtcNow;
//             _item.Modified_At = DateTime.UtcNow;
//             _item.EndTime = DateTime.UtcNow + Timer;
//             _item.Delivery_Status = DeliveryStatus.PENDING;

//             _dataContext.Orders.Add(_item);
//             return Save();
//         }

//         public async Task<bool> UpdateOrder(Order _item, Order patchOrder)
//         {
//             _item.Modified_At = DateTime.UtcNow;
//             _dataContext.Orders.Update(_item);
//             return Save();
//         }

//         public async Task<bool> DeleteOrder(Order _item)
//         {
//             _dataContext.Orders.Remove(_item);
//             return Save();
//         }

//         private bool Save()
//         {
//             int saved = _dataContext.SaveChanges();
//             return saved > 0;
//         }
//     }
// }
