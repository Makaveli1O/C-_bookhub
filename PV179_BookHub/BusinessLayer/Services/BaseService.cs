using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services;

public abstract class BaseService : IBaseService
{
    protected readonly IUnitOfWork _unitOfWork;
    
    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public virtual async Task SaveAsync(bool save = true)
    {
        if (save)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}

