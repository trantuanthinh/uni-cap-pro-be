using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
	public interface IOrder
	{
		ICollection<Order> GetOrders();
		Order GetOrder(Guid id);
		bool CreateOrder(Order item);
		bool UpdateOrder(Order _item, Order patchItem);
		bool DeleteOrder(Order item);
		bool Save();
	}
}
