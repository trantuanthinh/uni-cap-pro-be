using uni_cap_pro_be.Models;
using uni_cap_pro_be.Utils;

namespace uni_cap_pro_be.Interfaces
{
	public interface IOrderService<T> : IBaseService<T> where T : Order
	{
	}
}
