using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	public interface IDiscountService<T> : IBaseService<T> where T : Discount
	{
	}
}
