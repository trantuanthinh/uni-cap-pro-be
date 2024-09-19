using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	// DONE
	public interface IProduct_CategoryService
	{
		Task<ICollection<Product_Category>> GetProduct_Categories();
		Task<Product_Category> GetProduct_Category(Guid id);
		Task<bool> CreateProduct_Category(Product_Category item);
		Task<bool> UpdateProduct_Category(Product_Category item, Product_Category patchItem);
		Task<bool> DeleteProduct_Category(Product_Category item);
	}
}
