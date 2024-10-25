using System.Data;
using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
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
    public class OrderService(OrderRepository repository, Sub_OrderRepository sub_orderRepository)
    {
        private readonly OrderRepository _repository = repository;
        private readonly Sub_OrderRepository _sub_orderRepository = sub_orderRepository;

        public async Task<BaseResponse<OrderResponse>> GetOrders(QueryParameters queryParameters)
        {
            QueryParameterResult<Order> _items = _repository
                .SelectAll()
                .Include(item => item.Product)
                .ThenInclude(product => product.UnitMeasure)
                .Include(item => item.Product)
                .ThenInclude(product => product.Images)
                .Include(item => item.Product)
                .ThenInclude(product => product.Discount)
                .ThenInclude(discount => discount.Discount_Details)
                .Include(item => item.Sub_Orders)
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<OrderResponse> GetOrder(Guid id)
        {
            Order _item = _repository
                .SelectAll()
                .Include(item => item.Product)
                .ThenInclude(product => product.UnitMeasure)
                .Include(item => item.Product)
                .ThenInclude(product => product.Discount)
                .ThenInclude(discount => discount.Discount_Details)
                .Include(item => item.Sub_Orders)
                .Where(item => item.Id == id)
                .FirstOrDefault();
            if (_item == null)
            {
                return null;
            }
            return _item.ToResponse();
        }

        public async Task<bool> CreateOrder(Order _item)
        {
            if (_item.IsShare)
            {
                TimeSpan Timer = new(24, 0, 0);
                _item.EndTime = DateTime.UtcNow + Timer;
            }
            else
            {
                _item.EndTime = DateTime.UtcNow;
            }
            _item.Delivery_Status = DeliveryStatus.PENDING;
            _item.IsPaid = false;
            _item.Level = 1;
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateOrder(Guid id, PatchRequest<OrderRequest> patchRequest)
        {
            Order _item = _repository.SelectById(id);
            if (_item == null)
            {
                return false;
            }

            patchRequest.Patch(ref _item);
            _repository.Update(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteOrder(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }

        public async Task<Order> FindOrder(Guid orderId)
        {
            Order _item = _repository
                .SelectAll()
                .Include(item => item.Product)
                .ThenInclude(product => product.Discount)
                .ThenInclude(discount => discount.Discount_Details)
                .Include(item => item.Sub_Orders)
                .Where(item => item.Id == orderId)
                .FirstOrDefault();
            return _item;
        }

        public bool CheckValid(Order order)
        {
            return order.Level <= 5
                && !order.IsPaid
                && order.IsShare
                && order.EndTime > DateTime.UtcNow;
        }

        public async Task<bool> AddSubOrder(Order order)
        {
            order.Level += 1;
            double discount = 0;
            double total_price = 0;
            int total_quantity = 0;

            foreach (var discount_detail in order.Product.Discount.Discount_Details)
            {
                if (discount_detail.Level == order.Level)
                {
                    discount = discount_detail.Amount;
                    break;
                }
            }
            foreach (var sub_order in order.Sub_Orders)
            {
                sub_order.Price = order.Product.Price - (order.Product.Price * discount);
                _sub_orderRepository.Update(sub_order);
                bool checkSubOrder = _sub_orderRepository.Save();
                if (!checkSubOrder)
                {
                    total_price += sub_order.Price;
                    total_quantity += sub_order.Quantity;
                }
            }
            order.Total_Price = total_price;
            order.Total_Quantity = total_quantity;

            _repository.Update(order);
            return _repository.Save();
        }
    }
}
