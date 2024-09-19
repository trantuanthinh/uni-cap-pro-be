using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // TODO
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

        // public async Task<bool> UpdateUser(User _item, User patchItem)
        // {
        //     _item.Modified_At = DateTime.UtcNow;
        //     _item.Username = patchItem.Username;
        //     _item.Name = patchItem.Name;
        //     _item.Email = patchItem.Email;
        //     if (patchItem.Password != null)
        //     {
        //         patchItem.Password = _sharedService.HashPassword(patchItem.Password);
        //         _item.Password = patchItem.Password;
        //     }
        //     _item.PhoneNumber = patchItem.PhoneNumber;
        //     _item.Active_Status = patchItem.Active_Status;
        //     _item.User_Type = patchItem.User_Type;
        //     _item.Description = patchItem.Description;
        //     _item.Avatar = patchItem.Avatar;

        //     _dataContext.Users.Update(_item);
        //     return Save();
        // }

        // public async Task<bool> DeleteUser(User _item)
        // {
        //     _dataContext.Users.Remove(_item);
        //     return Save();
        // }
    }
}
