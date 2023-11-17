using AutoMapper;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services;

public class BaseService : IBaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task SaveAsync(bool save)
    {
        if (save)
        {
            await _unitOfWork.CommitAsync();
        }
    }
}

