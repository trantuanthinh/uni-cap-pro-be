using Core.Data.Base.Entity;
using Microsoft.EntityFrameworkCore;

namespace uni_cap_pro_be.Core.Base.Repository
{
    public class BaseRepository<T, DbContextType> : IBaseRepository<T>
        where T : BaseEntity<Guid>
        where DbContextType : DbContext
    {
        protected readonly DbContextType _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContextType context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> SelectAll()
        {
            return _dbSet;
        }

        public DbSet<T> GetDbSet()
        {
            return _dbSet;
        }

        public T Add(T obj)
        {
            return _dbSet.Add(obj).Entity;
        }

        public T Update(T obj)
        {
            return _dbSet.Update(obj).Entity;
        }

        public T Delete(T obj)
        {
            return _dbSet.Remove(obj).Entity;
        }

        // public bool IsExisted(string id)
        // {
        //     T obj = SelectById(id);
        //     return obj != null;
        // }

        // public T SelectById(string id)
        // {
        //     return _dbSet.Find(id);
        // }

        public bool Save()
        {
            int saved = _dbContext.SaveChanges();
            return saved > 0;
        }
    }
}
