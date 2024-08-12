namespace uni_cap_pro_be.Interfaces
{
	public interface Order_Detail
	{
		ICollection<Order_Detail> GetOrder_Details();
		Order_Detail GetOrder_Detail(Guid id);
		bool CreateOrder_Detail(Order_Detail item);
		bool UpdateOrder_Detail(Order_Detail _item, Order_Detail patchItem);
		bool DeleteOrder_Detail(Order_Detail item);
		bool Save();
	}
}
