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
    public class CommentService(CommentRepository repository) : BaseService()
    {
        private readonly CommentRepository _repository = repository;

        public async Task<BaseResponse<Comment>> GetComments(QueryParameters queryParameters)
        {
            QueryParameterResult<Comment> _items = _repository
                .SelectAll()
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item)
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<bool> CreateComment(Comment _item)
        {
            _item.Created_At = DateTime.UtcNow;
            _item.Modified_At = DateTime.UtcNow;
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateComment(Guid id, PatchRequest<CommentRequest> patchRequest)
        {
            var _item = _repository.SelectById(id);
            if (_item == null)
            {
                return false;
            }

            patchRequest.Patch(ref _item);
            _repository.Update(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
