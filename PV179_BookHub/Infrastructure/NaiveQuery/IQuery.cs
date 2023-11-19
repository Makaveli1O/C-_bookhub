
using System.Linq.Expressions;

namespace Infrastructure.NaiveQuery;

public interface IQuery<TEntity> where TEntity : class
{
    void Include(params Expression<Func<TEntity, object?>>[] includes);
    void Where(Expression<Func<TEntity, bool>>? filter = null);
    void SortBy(string sortAccordingTo, bool ascending);
    void Page(int pageToFetch, int pageSize);
    Task<QueryResult<TEntity>> ExecuteAsync();
}
