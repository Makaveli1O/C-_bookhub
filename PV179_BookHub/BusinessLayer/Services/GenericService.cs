using BusinessLayer.Exceptions;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services;

public class GenericService<TEntity, TKey> : BaseService, IGenericService<TEntity, TKey> where TEntity : class
{
    public readonly IGenericRepository<TEntity, TKey> Repository;

    public GenericService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        Repository = _unitOfWork.GetRepositoryByEntity<TEntity, TKey>();
    }

    public virtual async Task<TEntity> CreateEntityAsync(TEntity entity, bool save = true)
    {
        await Repository.AddAsync(entity);
        await SaveAsync(save);

        return entity;
    }

    public virtual async Task<TEntity> UpdateEntityAsync(TEntity entity, bool save = true)
    {
        Repository.Update(entity);
        await SaveAsync(save);

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> FetchAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public virtual async Task<TEntity> FindByIdAsync(TKey id)
    {
        var entity = await Repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NoSuchEntityException<TKey>(typeof(TEntity), id);
        }
        return entity;
    }

    public virtual async Task DeleteEntityAsync(TEntity entity, bool save = true)
    {
        Repository.Delete(entity);
        await SaveAsync(save);
    }
}
