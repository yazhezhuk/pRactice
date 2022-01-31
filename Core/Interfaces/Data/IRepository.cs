using Core.Domains;

namespace Core.Interfaces.Data;

public interface IRepository<TKey,TEntity> where TEntity : BaseEntity<TKey>
{
	TEntity Get(TKey id);

	IReadOnlyCollection<TEntity> GetAll();

	int Delete(TEntity entity);
	int Delete(TKey id);

	int Add(TEntity entity);

	int Update(TEntity entity);

	IReadOnlyCollection<TEntity> GetBySpecification(ISpecification<TEntity,IReadOnlyCollection<TEntity>> specification);


}
