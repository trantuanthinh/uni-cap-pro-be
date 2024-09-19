using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	// DONE
	public interface IProductService
	{
		Task<ICollection<Product>> GetProducts();
		Task<Product> GetProduct(Guid id);
		Task<bool> CreateProduct(Product item);
		Task<bool> UpdateProduct(Product item, Product patchItem);
		Task<bool> DeleteProduct(Product item);
		// public Task<List<Product>> GetSubOrdersById(Guid OrderId);
	}
}
