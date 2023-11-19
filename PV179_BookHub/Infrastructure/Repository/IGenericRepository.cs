using System.Linq.Expressions;

namespace Infrastructure.Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> AsQueryable();
    Task AddAsync(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity?> GetByIdAsync(long id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task SaveChangesAsync();
    Task<IEnumerable<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>>? filter);
    Task<TEntity?> GetSingleFilteredAsync(Expression<Func<TEntity, bool>>? filter);
}
