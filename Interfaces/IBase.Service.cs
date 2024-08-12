namespace uni_cap_pro_be.Interfaces
{
	public interface IBaseService<T>
	{
		ICollection<T> GetItems();
		T GetItem(Guid id);
		bool CreateItem(T item);
		bool UpdateItem(T item, T patchItem);
		bool DeleteItem(T item);
		bool Save();
	}
}
