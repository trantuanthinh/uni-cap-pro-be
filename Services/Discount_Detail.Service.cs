using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.Base.Entity;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.DTO.Request;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // DONE
    public class Discount_DetailService(Discount_DetailRepository repository) : BaseService
    {
        private readonly Discount_DetailRepository _repository = repository;

        // public async Task<List<Discount_DetailResponse>> GetDetailsByDiscountId(
        //     QueryParameters queryParameters
        // )
        // {
        //     QueryParameterResult<Discount_Detail> _items = _repository
        //         .SelectAll()
        //         .ApplyQueryParameters(queryParameters);

        //     return _items
        //         .Data.AsEnumerable()
        //         .Select(item => item.ToResponse())
        //         .ToList()
        //         .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        // }

        public async Task<BaseResponse<Discount_DetailResponse>> GetDiscount_Details(
            QueryParameters queryParameters
        )
        {
            QueryParameterResult<Discount_Detail> _items = _repository
                .SelectAll()
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<Discount_DetailResponse> GetDiscount_Detail(Guid id)
        {
            Discount_Detail _item = _repository
                .SelectAll()
                .Where(item => item.Id == id)
                .FirstOrDefault();
            return _item.ToResponse();
        }

        public async Task<bool> CreateDiscount_Detail(Discount_Detail _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateDiscount_Detail(
            Guid id,
            PatchRequest<Discount_DetailRequest> patchRequest
        )
        {
            Discount_Detail _item = _repository
                .SelectAll()
                .Where(item => item.Id == id)
                .FirstOrDefault();
            if (_item == null)
            {
                return false;
            }

            patchRequest.Patch(ref _item);
            _repository.Update(_item);
            return _repository.Save();
        }

        public async Task<bool> DeleteDiscount_Detail(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
