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

namespace uni_cap_pro_be.Services
{
    // DONE
    public class DiscountService(DiscountRepository repository) : BaseService()
    {
        private readonly DiscountRepository _repository = repository;

        public async Task<BaseResponse<DiscountResponse>> GetDiscounts(
            QueryParameters queryParameters
        )
        {
            QueryParameterResult<Discount> _items = _repository
                .SelectAll()
                .Include(item => item.Discount_Details)
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<DiscountResponse> GetDiscount(Guid id)
        {
            Discount _item =
                _repository
                    .SelectAll()
                    .Include(item => item.Discount_Details)
                    .Where(item => item.Id == id)
                    .FirstOrDefault() ?? throw new NotFoundException("Discount not found");
            return _item.ToResponse();
        }

        public async Task<bool> CreateDiscount(Discount _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateDiscount(Guid id, PatchRequest<DiscountRequest> patchRequest)
        {
            Discount _item = _repository.SelectById(id);
            if (_item == null)
            {
                return false;
            }

            patchRequest.Patch(ref _item);
            _repository.Update(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteDiscount(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
