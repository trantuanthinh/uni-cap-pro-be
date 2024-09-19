using AutoMapper;
using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Core;
using uni_cap_pro_be.Core.QueryParameter;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.Response;
using uni_cap_pro_be.Extensions;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Repositories;

namespace uni_cap_pro_be.Services
{
    // TODO
    public class ProductService(ProductRepository repository) : BaseService()
    {
        private readonly ProductRepository _repository = repository;

        public async Task<BaseResponse<ProductResponse>> GetProducts(
            QueryParameters queryParameters
        )
        {
            QueryParameterResult<Product> _items = _repository
                .SelectAll()
                .Include(item => item.Owner)
                .Include(item => item.Category)
                .Include(item => item.Discount)
                .Include(item => item.Images)
                .ApplyQueryParameters(queryParameters);

            return _items
                .Data.AsEnumerable()
                .Select(item => item.ToResponse())
                .ToList()
                .GetBaseResponse(_items.Page, _items.PageSize, _items.TotalRecords);
        }

        public async Task<ProductResponse> GetProduct(Guid id)
        {
            var _item = await _repository
                .GetDbSet()
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync();
            return _item.ToResponse();
        }

        // public async Task<bool> CreateProduct(Product _item)
        // {
        //     _item.Created_At = DateTime.UtcNow;
        //     _item.Modified_At = DateTime.UtcNow;
        //     _dataContext.Products.Add(_item);
        //     return Save();
        // }

        // public async Task<bool> UpdateProduct(Product _item, Product patchItem)
        // {
        //     _item.Modified_At = DateTime.UtcNow;
        //     _dataContext.Products.Update(_item);
        //     return Save();
        // }

        // public async Task<bool> DeleteProduct(Product _item)
        // {
        //     _dataContext.Products.Remove(_item);
        //     return Save();
        // }
    }
}
