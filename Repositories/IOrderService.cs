using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Repositories
{
	// DONE
	public interface IOrderService
	{
		Task<ICollection<Order>> GetOrders();
		Task<Order> GetOrder(Guid id);
		Task<bool> CreateOrder(Order item);
		Task<bool> UpdateOrder(Order item, Order patchItem);
		Task<bool> DeleteOrder(Order item);
	}
}
