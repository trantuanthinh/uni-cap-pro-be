using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IProduct_CategoryService
	{
		ICollection<Product_Category> GetProduct_Categories();
		Product_Category GetProduct_Category(Guid id);
		bool CreateProduct_Category(Product_Category item);
		bool UpdateProduct_Category(Product_Category _item, Product_Category patchItem);
		bool DeleteProduct_Category(Product_Category item);
		bool Save();
	}
}
