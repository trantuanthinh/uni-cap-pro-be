using Core.Data.Base.Entity;

namespace uni_cap_pro_be.Core.Base.Repository
{
	public interface IBaseRepository<T>
		where T : BaseEntity<Guid>
	{
		public IQueryable<T> SelectAll();
	}
}
