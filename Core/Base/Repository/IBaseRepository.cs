using uni_cap_pro_be.Core.Data.Base.Entity;

namespace uni_cap_pro_be.Core.Base.Repository
{
    // DONE
    public interface IBaseRepository<T>
        where T : BaseEntity<Guid>
    {
        public IQueryable<T> SelectAll();
        public T SelectById(Guid id);
        public T Add(T obj);
        public T Update(T obj);
        public T Delete(Guid id);
        public bool Save();
        public Task<bool> SaveAsync();
    }

    public interface IBaseRepositoryAddress<T>
        where T : BaseEntity<string>
    {
        public IQueryable<T> SelectAll();
        public T SelectById(string id);
    }
}
