namespace Core.Interfaces.Data;

public class QueryBuilder<T,TResult> : ISpecificationBuilder<T,TResult>
{
	public Specification<T, TResult> Query { get; }

	public void Combine(ISpecification<T,TResult> other)
	{
		Query.Selector = Query.Selector.Union(other.Selector);
	}
}
