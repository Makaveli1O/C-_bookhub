using System.Linq.Expressions;

namespace Infrastructure.NaiveQuery.Filters;

public interface IFilter<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> CreateExpression();
}
