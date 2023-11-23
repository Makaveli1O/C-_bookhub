using AutoMapper;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services;

public abstract class BaseService<TEntity, TKey> : IBaseService where TEntity : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    public readonly IGenericRepository<TEntity, TKey> Repository;
    public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        Repository = _unitOfWork.GetRepositoryByEntity<TEntity, TKey>();
    }

    public virtual async Task SaveAsync(bool save = true)
    {
        if (save)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}

