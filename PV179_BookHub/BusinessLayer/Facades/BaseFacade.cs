using AutoMapper;

namespace BusinessLayer.Facades;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlTypes;

public abstract class BaseFacade
{
    protected readonly IMapper _mapper;
    protected readonly IMemoryCache? _memoryCache;
    private readonly string? _memoryCacheKey;

    public BaseFacade(IMapper mapper, IMemoryCache? memoryCache, string? memoryCacheKey)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _memoryCacheKey = memoryCacheKey;
    }

    protected string GetMemoryCacheKey(long? anotherPartOfKey)
    {
        var key = _memoryCacheKey ?? "";
        if (anotherPartOfKey != null)
        {
            key += anotherPartOfKey;
        }
        return key;
    }
}
