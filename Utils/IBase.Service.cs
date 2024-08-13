namespace uni_cap_pro_be.Utils
{
    public interface IBaseService<T>
    {
        Task<ICollection<T>> GetItems();
        Task<T> GetItem(Guid id);
        Task<bool> CreateItem(T item);
        Task<bool> UpdateItem(T item, T patchItem);
        Task<bool> DeleteItem(T item);
        bool Save();
    }
}
