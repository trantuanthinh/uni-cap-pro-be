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
    public class Product_CategoryService(Product_CategoryRepository repository) : BaseService()
    {
        private readonly Product_CategoryRepository _repository = repository;

        public async Task<BaseResponse<Product_CategoryResponse>> GetProduct_Categories(
            QueryParameters queryParameters
        )
        {
            QueryParameterResult<Product_Category> _items = _repository
                .SelectAll()
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<Product_CategoryResponse> GetProduct_Category(Guid id)
        {
            Product_Category _item =
                await _repository.SelectAll().Where(item => item.Id == id).FirstOrDefaultAsync()
                ?? throw new NotFoundException("Product_Category not found");
            return _item.ToResponse();
        }

        public async Task<bool> CreateProduct_Category(Product_Category _item)
        {
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateProduct_Category(
            Guid id,
            PatchRequest<Product_CategoryRequest> patchRequest
        )
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

        public async Task<bool> DeleteProduct_Category(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }
    }
}
