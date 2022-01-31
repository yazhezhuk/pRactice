using Ardalis.GuardClauses;
using Core.Domains;
using Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public abstract class BaseEfRepository<T> : IRepository<int,T> where T : BaseEntity<int>
{

	protected readonly DbContext _dataContext;

	public BaseEfRepository(DbContext dataContext) => _dataContext = dataContext;

	public virtual IReadOnlyCollection<T> GetAll() =>
		_dataContext.Set<T>().ToList();

	public virtual T Get(int id)
	{
		var entry = _dataContext.Set<T>()
			.Find(id);
		return Guard.Against.Null(entry, nameof(entry))!;
	}

	public virtual int Delete(int id)
	{
		var entry = _dataContext.Set<T>().Find(id);
		_dataContext.Set<T>()
			.Remove(Guard.Against.Null(entry,nameof(entry))!);

		return _dataContext.SaveChanges();
	}

	public virtual int Delete(T entity)
	{
		_dataContext.Set<T>()
			.Remove(Guard.Against.Null(entity, nameof(entity)));

		return _dataContext.SaveChanges();
	}

	public virtual int Add(T entity)
	{
		_dataContext.Set<T>()
			.Add(entity);

		return _dataContext.SaveChanges();
	}

	public virtual int Update(T entity)
	{
		_dataContext.Set<T>().Update(entity);

		return _dataContext.SaveChanges();
	}

	public IReadOnlyCollection<T> GetBySpecification(ISpecification<T,IReadOnlyCollection<T>> specification)
	{
		return specification.Execute();

	}
}
