using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	public interface IProduct_ImageService<T> : IBaseService<T> where T : Product_Image
	{
		Task<ICollection<T>> GetImages(Guid ProductId);
	}
}
