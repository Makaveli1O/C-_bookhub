using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> WhereCheck<TEntity>(
        this IQueryable<TEntity> query, 
        Expression<Func<TEntity, bool>>? filter
        ) where TEntity : class
    {
        return filter == null ? query : query.WhereCheck(filter);
    }

    public static IQueryable<TEntity> IncludeMultipleCheck<TEntity>(
        this IQueryable<TEntity> query, 
        Expression<Func<TEntity, object>>[] includes
        ) where TEntity : class
    {
        return includes == null ? query : includes.Aggregate(query, (current, include) => current.Include(include));
    }
}
