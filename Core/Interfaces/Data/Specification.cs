using System.Linq.Expressions;

namespace Core.Interfaces.Data;

public abstract class Specification<T,TResult> : ISpecification<T,TResult>
{
	public IQueryable<T> Selector { get; set; }

	public Expression<Func<IQueryable<T>, TResult>> Expression { get; set; }


	public Specification(Expression<Func<IQueryable<T>, TResult>> expression)
	{
		Expression = expression;
	}


	public TResult Execute()
	{
		return Expression.Compile()(Selector);
	}

}
