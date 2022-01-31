namespace Core.Interfaces.Data;

public interface ISpecification<TEntity,TResult>
{
	IQueryable<TEntity> Selector { get; }
	TResult Execute();
}
