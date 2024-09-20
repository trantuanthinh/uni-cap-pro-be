using System.Data;
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
    public class OrderService(OrderRepository repository)
    {
        private readonly OrderRepository _repository = repository;

        public async Task<BaseResponse<OrderResponse>> GetOrders(QueryParameters queryParameters)
        {
            QueryParameterResult<Order> _items = _repository
                .SelectAll()
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<OrderResponse> GetOrder(Guid id)
        {
            Order _item = _repository.SelectAll().Where(item => item.Id == id).FirstOrDefault();
            return _item.ToResponse();
        }

        public async Task<bool> CreateOrder(Order _item)
        {
            TimeSpan Timer = new(24, 0, 0);

            _item.EndTime = DateTime.UtcNow + Timer;
            _item.Delivery_Status = DeliveryStatus.PENDING;
            _item.IsPaid = false;
            _item.Level = 1;
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateOrder(Guid id, PatchRequest<OrderRequest> patchRequest)
        {
            Order _item = _repository.SelectAll().Where(item => item.Id == id).FirstOrDefault();
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
    }
}
