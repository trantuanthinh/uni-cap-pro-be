using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IProductService
	{
		ICollection<Product> GetProducts();
		Product GetProduct(Guid id);
		bool CreateProduct(Product item);
		bool UpdateProduct(Product _item, Product patchItem);
		bool DeleteProduct(Product item);
		bool Save();
	}
}
