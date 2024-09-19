using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	// DONE
	public interface IProduct_ImageService
	{
		Task<ICollection<Product_Image>> GetImages(Guid OwnerId);
		Task<Product_Image> GetImage(Guid id);
		Task<bool> CreateImage(Product_Image item);
		Task<bool> DeleteImage(Product_Image item);
		Task<List<string>> GetImagesURLByProductId(Guid OwnerId, Guid ProductId);
	}
}
