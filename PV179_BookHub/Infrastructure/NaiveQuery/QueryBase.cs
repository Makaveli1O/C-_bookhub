using DataAccessLayer.Models;
using Infrastructure.NaiveQuery.Filters;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.NaiveQuery;

public class QueryBase<TEntity, TKey> : IQuery<TEntity> where TEntity : class
{
    private IQueryable<TEntity> _query;

    public IUnitOfWork UnitOfWork {  get; set; }
    public IFilter<TEntity>? Filter { get; set; }
    public int PageSize { get; set; } = 20;
    public int CurrentPage { get; set; }
    public string? SortAccordingTo { get; set; }
    public bool UseAscendingOrder { get; set; }


    public QueryBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        _query = unitOfWork
            .GetRepositoryByEntity<TEntity, TKey>()
            .AsQueryable();
    }

    public async Task<QueryResult<TEntity>> ExecuteAsync()
    {
        var result = await _query.ToListAsync();

        var queryResult = new QueryResult<TEntity>()
        {
            TotalItemsCount = result.Count(),
            Items = result,
            PageSize = PageSize,
            PagingEnabled = true,
            RequestedPageNumber = CurrentPage
        };

        return queryResult;
    }

    public void Page(int pageToFetch, int pageSize)
    {
        _query = _query.Skip((pageToFetch - 1) * pageSize).Take(pageSize);
    }

    public void SortBy(string sortAccordingTo, bool ascending)
    {
        if (sortAccordingTo == string.Empty)
        {
            return;
        }

        //https://stackoverflow.com/questions/1689199/c-sharp-code-to-order-by-a-property-using-the-property-name-as-a-string/67630860#67630860
        var param = Expression.Parameter(typeof(TEntity));
        var memberAccess = Expression.Property(param, sortAccordingTo);
        var convertedMemberAccess = Expression.Convert(memberAccess, typeof(object));
        var orderPredicate = Expression.Lambda<Func<TEntity, object>>(convertedMemberAccess, param);


        if (ascending)
        {
            _query = _query.OrderBy(orderPredicate);
        }
        else
        {
            _query = _query.OrderByDescending(orderPredicate);
        }
    }

    public void Where(Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter != null)
        {
            _query = _query.Where(filter);
        }
    }

    public void Include(params Expression<Func<TEntity, object?>>[] includes)
    {
        if (includes != null)
        {
            _query = includes
                .Aggregate(_query, (current, include) => current.Include(include));
        }
    }
}
