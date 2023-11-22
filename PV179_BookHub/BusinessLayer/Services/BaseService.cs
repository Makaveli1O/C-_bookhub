using AutoMapper;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services;

public class BaseService : IBaseService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task SaveAsync(bool save = true)
    {
        if (save)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}

