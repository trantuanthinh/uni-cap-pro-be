using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class UserService(
        UserRepository repository,
        OrderRepository orderRepository,
        Sub_OrderRepository sub_orderRepository
    ) : BaseService()
    {
        private readonly UserRepository _repository = repository;
        private readonly OrderRepository _orderRepository = orderRepository;
        private readonly Sub_OrderRepository _sub_orderRepository = sub_orderRepository;

        public async Task<BaseResponse<UserResponse>> GetUsers(QueryParameters queryParameters)
        {
            QueryParameterResult<User> _items = _repository
                .SelectAll()
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<UserResponse> GetUser(Guid id)
        {
            User _item = _repository.SelectAll().Where(item => item.Id == id).FirstOrDefault();
            return _item.ToResponse();
        }

        public async Task<bool> UpdateUser(Guid id, PatchRequest<UserRequest> patchRequest)
        {
            User _item = _repository.SelectAll().Where(item => item.Id == id).FirstOrDefault();
            if (_item == null)
            {
                return false;
            }

            patchRequest.Patch(ref _item);
            _repository.Update(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }

        public async Task<BaseResponse<UserSub_OrderResponse>> GetUserOrders(
            Guid id,
            QueryParameters queryParameters
        )
        {
            // UserSub_OrderResponse _response = new UserSub_OrderResponse { };

            // Fetch sub-orders associated with the user
            var subOrders = _sub_orderRepository
                .SelectAll()
                .Where(order => order.UserId == id)
                .ToList();

            // Track unique OrderIds from sub-orders
            List<Guid> orderIds = subOrders
                .Select(subOrder => subOrder.OrderId)
                .Distinct()
                .ToList();

            // Fetch and apply query parameters on main orders using the tracked OrderIds
            QueryParameterResult<Order> _items = _orderRepository
                .SelectAll()
                .Include(item => item.Product)
                .ThenInclude(product => product.Images)
                .Include(item => item.Product)
                .Where(order => orderIds.Contains(order.Id))
                .ApplyQueryParameters(queryParameters);

            // Return paginated order responses
            // return _items
            //     .Data.AsEnumerable()
            //     .Select(item => item.ToResponse())
            //     .ToList()
            //     .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
            return null;
        }
    }
}
