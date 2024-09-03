using Microsoft.EntityFrameworkCore;
using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	// TODO
	public class ProductService<T> : IProductService<T> where T : Product
	{
		private readonly DataContext _dataContext;
		private readonly DbSet<T> _dataSet;
		private readonly SharedService _sharedService;
		private readonly IProduct_ImageService<Product_Image> _imageSerivce;
		private readonly IDiscountService<Discount> _discountService;

		public ProductService(DataContext dataContext,
			SharedService sharedService,
			IProduct_ImageService<Product_Image> imageSerivce,
			IDiscountService<Discount> discountService)
		{
			_dataContext = dataContext;
			_dataSet = _dataContext.Set<T>();
			_sharedService = sharedService;
			_imageSerivce = imageSerivce;
			_discountService = discountService;
		}

		public async Task<ICollection<T>> GetItems()
		{
			var _items = await _dataSet.OrderBy(item => item.Id).ToListAsync();
			foreach (var item in _items)
			{
				List<string> imagesName = await _imageSerivce.GetImagesURLByProductId(item.OwnerId, item.Id);
				item.Images = imagesName;

				Discount discount = await _discountService.GetItem(item.DiscountId);
				item.Discount = discount;
			}
			return _items;
		}

		public async Task<T> GetItem(Guid id)
		{
			T _item = await _dataSet.SingleOrDefaultAsync(item => item.Id == id);
			List<string> imagesName = await _imageSerivce.GetImagesURLByProductId(_item.OwnerId, _item.Id);
			_item.Images = imagesName;

			Discount discount = await _discountService.GetItem(_item.DiscountId);
			_item.Discount = discount;
			return _item;
		}

		// TODO
		public async Task<bool> CreateItem(T _item)
		{
			_item.Created_At = DateTime.UtcNow;
			_item.Modified_At = DateTime.UtcNow;
			_dataSet.Add(_item);
			return Save();
		}

		// TODO
		public async Task<bool> UpdateItem(T _item, T patchItem)
		{

			_item.Modified_At = DateTime.UtcNow;
			_dataSet.Update(_item);
			return Save();
		}

		public async Task<bool> DeleteItem(T _item)
		{
			_dataSet.Remove(_item);
			return Save();
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
