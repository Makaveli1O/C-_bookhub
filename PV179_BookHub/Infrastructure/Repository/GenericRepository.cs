using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected readonly BookHubDbContext _dbContext;
    public string KeyName { get; } = "id";

    public GenericRepository(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual IQueryable<TEntity> AsQueryable()
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }
    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task AddRangeAsync(params TEntity[] entities)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
        )
    {
        return await _dbContext
            .Set<TEntity>()
            .AsQueryable()
            .IncludeMultipleCheck(includes)
            .WhereCheck(filter)
            .ToListAsync();
    }

    public virtual async Task<TEntity?> GetSingleAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        params Expression<Func<TEntity, object>>[] includes
        )
    {
        return await _dbContext
            .Set<TEntity>()
            .AsQueryable()
            .IncludeMultipleCheck(includes)
            .WhereCheck(filter)
            .FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        TKey id,
        params Expression<Func<TEntity, object>>[] includes
        )
    {
        var param = Expression.Parameter(typeof(TEntity), "source");
        var constant = Expression.Constant(id);
        MemberExpression member = Expression.Property(param, KeyName);

        return await _dbContext
            .Set<TEntity>()
            .AsQueryable()
            .IncludeMultipleCheck(includes)
            .FirstOrDefaultAsync(Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, constant), param));
    }

    public virtual async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    } 
}
