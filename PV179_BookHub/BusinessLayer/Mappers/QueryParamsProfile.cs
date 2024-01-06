using AutoMapper;
using BusinessLayer.DTOs;
using Infrastructure.Query;

namespace BusinessLayer.Mappers;

public class QueryParamsProfile : Profile
{
    public QueryParamsProfile()
    {
        CreateMap<BaseFilterDto, QueryParams>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
