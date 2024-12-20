﻿using Microsoft.EntityFrameworkCore;
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
                .Include(item => item.UnitMeasure)
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
            Product _item =
                await _repository
                    .SelectAll()
                    .Include(item => item.Owner)
                    .Include(item => item.Category)
                    .Include(item => item.UnitMeasure)
                    .Include(item => item.Images)
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync() ?? throw new NotFoundException("Product not found");
            return _item.ToResponse();
        }

        public async Task<bool> CreateProduct(Product _item)
        {
            _item.Total_Rating_Value = 0;
            _item.Total_Rating_Quantity = 0;
            _repository.Add(_item);
            return _repository.Save();
        }

        public async Task<bool> UpdateProduct(Guid id, PatchRequest<ProductRequest> patchRequest)
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

        public async Task<bool> DeleteProduct(Guid id)
        {
            _repository.Delete(id);
            return _repository.Save();
        }

        public async Task<bool> UpdateProductRating(Guid id, int rating)
        {
            Product _item =
                _repository.SelectById(id) ?? throw new NotFoundException("Product not found");
            _item.Total_Rating_Value += rating;
            _item.Total_Rating_Quantity += 1;
            _repository.Update(_item);
            return await _repository.SaveAsync();
        }
    }
}
