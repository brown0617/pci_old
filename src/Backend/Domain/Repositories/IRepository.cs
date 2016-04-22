using System.Collections.Generic;

namespace Backend.Domain.Repositories
{
	public interface IRepository<TEntity, in TKey> where TEntity : class
	{
		IEnumerable<TEntity> Get();
		TEntity Get(TKey id);
		TEntity New();
		TEntity Save(TEntity entity);
		void Delete(TEntity entity);
	}
}