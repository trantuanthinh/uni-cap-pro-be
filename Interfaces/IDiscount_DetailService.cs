using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	// DONE
	public interface IDiscount_DetailService<T> : IBaseService<T> where T : Discount_Detail
	{
		Task<List<T>> GetDetailsById(Guid DiscountId);
	}
}
