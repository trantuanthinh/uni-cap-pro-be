using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	public class Product_CategoryService(DataContext dataContext, SharedService sharedService) : IProduct_CategoryService
	{
		public readonly DataContext _dataContext = dataContext;
		public readonly SharedService _sharedService = sharedService;

		public ICollection<Product_Category> GetProduct_Categories()
		{
			return _dataContext.Product_Categories.OrderBy(item => item.Id).ToList();
		}

		public Product_Category GetProduct_Category(Guid id)
		{
			Product_Category _item = _dataContext.Product_Categories.SingleOrDefault(item => item.Id == id);
			return _item;
		}

		public bool CreateProduct_Category(Product_Category _item)
		{
			_item.Created_At = DateTime.UtcNow;
			_item.Modified_At = DateTime.UtcNow;
			_dataContext.Product_Categories.Add(_item);
			return Save();
		}

		public bool UpdateProduct_Category(Product_Category _item, Product_Category patchItem)
		{

			_item.Modified_At = DateTime.UtcNow;
			_dataContext.Product_Categories.Update(_item);
			return Save();
		}

		public bool DeleteProduct_Category(Product_Category _item)
		{
			_dataContext.Product_Categories.Remove(_item);
			return Save();
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}

	}
}
