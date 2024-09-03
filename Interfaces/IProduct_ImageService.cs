using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	// DONE
	public interface IProduct_ImageService<T> : IBaseService<T> where T : Product_Image
	{
		Task<ICollection<T>> GetItems(Guid OwnerId);
		Task<T> GetItem(Guid id, Guid OwnerId);
		Task<List<string>> GetImagesURLByProductId(Guid OwnerId, Guid ProductId);
	}
}
