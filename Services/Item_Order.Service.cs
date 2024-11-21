using System.Data;
using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.Exceptions;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Item_OrderService(Item_OrderRepository repository)
    {
        private readonly Item_OrderRepository _repository = repository;

        public async Task<bool> CreateItem_Order(Item_Order _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        // public async Task<bool> UpdateOrder(Guid id, PatchRequest<OrderRequest> patchRequest)
        // {
        //     Order _item = _repository.SelectById(id);
        //     if (_item == null)
        //     {
        //         return false;
        //     }

        //     patchRequest.Patch(ref _item);
        //     _repository.Update(_item);
        //     return _repository.Save();
        // }

        // public async Task<bool> DeleteOrder(Guid id)
        // {
        //     _repository.Delete(id);
        //     return _repository.Save();
        // }

        // public async Task<Order> FindOrder(Guid orderId)
        // {
        //     Order _item = _repository
        //         .SelectAll()
        //         .Include(item => item.Sub_Orders)
        //         .Where(item => item.Id == orderId)
        //         .FirstOrDefault();
        //     return _item;
        // }

        // public bool CheckValid(Order order)
        // {
        //     return order.Level <= 5 && order.IsShare && order.EndTime > DateTime.UtcNow;
        // }

        // public async Task<bool> AddSubOrder(Order order)
        // {
        //     order.Level += 1;
        //     // double discount = 0;
        //     // double total_price = 0;
        //     // int total_quantity = 0;

        //     // foreach (var discount_detail in order.Product.Discount.Discount_Details)
        //     // {
        //     //     if (discount_detail.Level == order.Level)
        //     //     {
        //     //         discount = discount_detail.Amount;
        //     //         break;
        //     //     }
        //     // }
        //     // foreach (var sub_order in order.Sub_Orders)
        //     // {
        //     //     sub_order.Price = order.Product.Price - (order.Product.Price * discount);
        //     //     _sub_orderRepository.Update(sub_order);
        //     //     bool checkSubOrder = _sub_orderRepository.Save();
        //     //     if (!checkSubOrder)
        //     //     {
        //     //         total_price += sub_order.Price;
        //     //         total_quantity += sub_order.Quantity;
        //     //     }
        //     // }
        //     // order.Total_Price = total_price;
        //     // order.Total_Quantity = total_quantity;

        //     _repository.Update(order);
        //     return _repository.Save();
        // }
    }
}
