using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	// DONE
	public interface IProduct_ImageService<T> : IBaseService<T> where T : Product_Image
	{
		Task<List<string>> GetImagesURL(Guid ProductId);
	}
}
