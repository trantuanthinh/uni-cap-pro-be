using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IProduct_ImageService
	{
		ICollection<Product_Image> GetProduct_Images();
		Product_Image GetProduct_Image(Guid id);
		bool CreateProduct_Image(Product_Image item);
		bool UpdateProduct_Image(Product_Image _item, Product_Image patchItem);
		bool DeleteProduct_Image(Product_Image item);
		bool Save();
	}
}
