using uni_cap_pro_be.Data;
using uni_cap_pro_be.Interfaces;
using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Services
{
	public class ProductService(DataContext dataContext, SharedService sharedService) : IProductService
	{
		public readonly DataContext _dataContext = dataContext;
		public readonly SharedService _sharedService = sharedService;

		public ICollection<Product> GetProducts()
		{
			return _dataContext.Products.OrderBy(item => item.Id).ToList();
		}

		public Product GetProduct(Guid id)
		{
			Product _item = _dataContext.Products.SingleOrDefault(item => item.Id == id);
			return _item;
		}

		// TODO
		public bool CreateProduct(Product _item)
		{
			_item.Created_At = DateTime.UtcNow;
			_item.Modified_At = DateTime.UtcNow;
			_dataContext.Products.Add(_item);
			return Save();
		}

		// TODO
		public bool UpdateProduct(Product _item, Product patchItem)
		{

			_item.Modified_At = DateTime.UtcNow;
			_dataContext.Products.Update(_item);
			return Save();
		}

		public bool DeleteProduct(Product _item)
		{
			_dataContext.Products.Remove(_item);
			return Save();
		}

		public bool Save()
		{
			int saved = _dataContext.SaveChanges();
			return saved > 0;
		}
	}
}
