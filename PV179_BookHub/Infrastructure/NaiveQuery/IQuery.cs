using System.Linq.Expressions;

namespace Infrastructure.NaiveQuery;

public interface IQuery<TEntity, TKey> where TEntity : class
{
    IQuery<TEntity, TKey> Include(params Expression<Func<TEntity, object?>>[] includes);
    IQuery<TEntity, TKey> Where(Expression<Func<TEntity, bool>>? filter = null);
    IQuery<TEntity, TKey> SortBy(string sortAccordingTo, bool ascending);
    IQuery<TEntity, TKey> Page(int pageToFetch, int pageSize);
    Task<QueryResult<TEntity>> ExecuteAsync();
}
