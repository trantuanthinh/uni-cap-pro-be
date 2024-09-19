using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Sub_OrderService(Sub_OrderRepository repository) : BaseService()
    {
        private readonly Sub_OrderRepository _repository = repository;

        public async Task<BaseResponse<Sub_OrderResponse>> GetSub_Orders(
            QueryParameters queryParameters
        )
        {
            QueryParameterResult<Sub_Order> _items = _repository
                .SelectAll()
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<Sub_OrderResponse> GetSub_Order(Guid id)
        {
            Sub_Order _item = _repository.GetDbSet().Where(item => item.Id == id).FirstOrDefault();
            return _item.ToResponse();
        }

        public async Task<bool> CreateSub_Order(Sub_Order _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteSub_Order(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
