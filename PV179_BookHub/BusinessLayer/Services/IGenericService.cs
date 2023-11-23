

namespace BusinessLayer.Services;

public interface IGenericService<TEntity, TKey> : IBaseService where TEntity : class
{
    Task<TEntity> CreateEntityAsync(TEntity entity, bool save = true);
    Task<TEntity> UpdateEntityAsync(TEntity entity, bool save = true);
    Task<IEnumerable<TEntity>> FetchAllAsync();
    Task<TEntity> FindByIdAsync(TKey id);
    Task DeleteEntityAsync(TEntity entity, bool save = true);
}
