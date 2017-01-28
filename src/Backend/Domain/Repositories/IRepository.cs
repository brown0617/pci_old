using System.Collections.Generic;
using System.Linq;

namespace Backend.Domain.Repositories
{
	public interface IRepository<TEntity, in TKey> where TEntity : class
	{
		IQueryable<TEntity> Get();
		TEntity Get(TKey id);
		TEntity New();
		TEntity Save(TEntity entity);
		void Delete(TEntity entity);
	}
}