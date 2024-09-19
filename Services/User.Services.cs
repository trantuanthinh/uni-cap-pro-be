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
    public class UserService(UserRepository repository) : BaseService()
    {
        private readonly UserRepository _repository = repository;

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
            User _item = _repository.GetDbSet().Where(item => item.Id == id).FirstOrDefault();
            return _item.ToResponse();
        }

        public async Task<bool> UpdateUser(Guid id, PatchRequest<UserRequest> patchRequest)
        {
            var _item = _repository.GetDbSet().Where(item => item.Id == id).FirstOrDefault();
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
    }
}
