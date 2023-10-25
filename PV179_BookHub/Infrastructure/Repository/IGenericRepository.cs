using System.Linq.Expressions;

namespace Infrastructure.Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    TEntity? GetById(long id);
    Task<TEntity?> GetByIdAsync(long id);
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();
    void SaveChanges();
    Task SaveChangesAsync();
    Task<IEnumerable<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>>? filter);
}
