using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	public class Product_ImageService(DataContext dataContext, SharedService sharedService) : IProduct_ImageService
	{
		public readonly DataContext _dataContext = dataContext;
		public readonly SharedService _sharedService = sharedService;

		public ICollection<Product_Image> GetProduct_Images()
		{
			return _dataContext.Product_Images.OrderBy(item => item.Id).ToList();
		}

		public Product_Image GetProduct_Image(Guid id)
		{
			Product_Image _item = _dataContext.Product_Images.SingleOrDefault(item => item.Id == id);
			return _item;
		}

		public bool CreateProduct_Image(Product_Image _item)
		{
			_item.Created_At = DateTime.UtcNow;
			//_item.Modified_At = DateTime.UtcNow;
			_dataContext.Product_Images.Add(_item);
			return Save();
		}

		public bool UpdateProduct_Image(Product_Image _item, Product_Image patchItem)
		{

			//_item.Modified_At = DateTime.UtcNow;
			_dataContext.Product_Images.Update(_item);
			return Save();
		}

		public bool DeleteProduct_Image(Product_Image _item)
		{
			_dataContext.Product_Images.Remove(_item);
			return Save();
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
