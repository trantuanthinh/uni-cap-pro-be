using uni_cap_pro_be.Models;

namespace uni_cap_pro_be.Interfaces
{
    // DONE
    public interface ISub_OrderService
    {
        Task<ICollection<Sub_Order>> GetSub_Orders();
		Task<Sub_Order> GetSub_Order(Guid id);
		Task<bool> CreateSub_Order(Sub_Order item);
		Task<bool> UpdateSub_Order(Sub_Order item, Sub_Order patchItem);
		Task<bool> DeleteSub_Order(Sub_Order item);
        public Task<List<Sub_Order>> GetSubOrdersById(Guid OrderId);
    }
}
