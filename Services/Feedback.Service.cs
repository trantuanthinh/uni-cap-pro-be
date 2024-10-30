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
    public class FeedbackService(FeedbackRepository repository) : BaseService()
    {
        private readonly FeedbackRepository _repository = repository;

        public async Task<ICollection<FeedbackResponse>> GetFeedbacks(Guid productId)
        {
            ICollection<Feedback> _items = _repository
                .SelectAll()
                .Include(item => item.Sub_Order)
                .ThenInclude(sub_order => sub_order.User)
                .Where(item => item.ProductId == productId)
                .ToList();
            return _items.AsEnumerable().Select(item => item.ToResponse()).ToList();
        }

        public async Task<bool> CreateFeedback(Feedback _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateFeedback(Guid id, PatchRequest<FeedbackRequest> patchRequest)
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

        public async Task<bool> DeleteFeedback(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
