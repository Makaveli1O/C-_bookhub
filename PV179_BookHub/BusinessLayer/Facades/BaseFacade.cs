using AutoMapper;

namespace BusinessLayer.Facades;

public abstract class BaseFacade
{
    protected readonly IMapper _mapper;

    public BaseFacade(IMapper mapper)
    {
        _mapper = mapper;
    }
}
