using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly BookHubDbContext _dbContext;

    public GenericRepository(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }
    public virtual async Task<TEntity?> GetSingleFilteredAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

}
