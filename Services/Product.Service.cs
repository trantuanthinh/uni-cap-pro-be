using AutoMapper;
using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.DTO.UserDTO;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
    // TODO
    public class ProductService(
        DataContext dataContext,
        IMapper mapper,
        IUserService userSerivce,
        IProduct_ImageService imageSerivce,
        IProduct_CategoryService categorySerivce,
        IDiscountService discountService
    ) : IProductService
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userSerivce = userSerivce;
        private readonly IProduct_ImageService _imageSerivce = imageSerivce;
        private readonly IProduct_CategoryService _categorySerivce = categorySerivce;
        private readonly IDiscountService _discountSerivce = discountService;

        public async Task<ICollection<Product>> GetProducts()
        {
            var _items = await _dataContext.Products.OrderBy(item => item.Id).ToListAsync();
            foreach (var item in _items)
            {
                List<string> imagesName = await _imageSerivce.GetImagesURLByProductId(
                    item.OwnerId,
                    item.Id
                );
                item.Images = imagesName;

                User user = await _userSerivce.GetUser(item.OwnerId);
                // UserDTO userDTO = _mapper.Map<UserDTO>(user);
                // item.Owner = userDTO;

                Discount discount = await _discountSerivce.GetDiscount(item.DiscountId);
                item.Discount = discount;

                Product_Category category = await _categorySerivce.GetProduct_Category(
                    item.CategoryId
                );
                item.Category = category;
            }
            return _items;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            Product _item = await _dataContext.Products.SingleOrDefaultAsync(item => item.Id == id);
            List<string> imagesName = await _imageSerivce.GetImagesURLByProductId(
                _item.OwnerId,
                _item.Id
            );
            _item.Images = imagesName;

            User user = await _userSerivce.GetUser(_item.OwnerId);
            // UserDTO userDTO = _mapper.Map<UserDTO>(user);
            // _item.Owner = userDTO;

            Discount discount = await _discountSerivce.GetDiscount(_item.DiscountId);
            _item.Discount = discount;

            Product_Category category = await _categorySerivce.GetProduct_Category(
                _item.CategoryId
            );
            _item.Category = category;

            return _item;
        }

        public async Task<bool> CreateProduct(Product _item)
        {
            _item.Created_At = DateTime.UtcNow;
            _item.Modified_At = DateTime.UtcNow;
            _dataContext.Products.Add(_item);
            return Save();
        }

        public async Task<bool> UpdateProduct(Product _item, Product patchItem)
        {
            _item.Modified_At = DateTime.UtcNow;
            _dataContext.Products.Update(_item);
            return Save();
        }

        public async Task<bool> DeleteProduct(Product _item)
        {
            _dataContext.Products.Remove(_item);
            return Save();
        }

        private bool Save()
        {
            int saved = _dataContext.SaveChanges();
            return saved > 0;
        }
    }
}
