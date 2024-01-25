using AutoMapper;
using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.Filter;
using BusinessLayer.DTOs.Author.View;
using Infrastructure.Query.Filters.EntityFilters;

namespace BusinessLayer.Mappers.Enitity;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<CreateAuthorDto, AuthorEntity>();
        CreateMap<AuthorEntity, GeneralAuthorViewDto>();
        CreateMap<AuthorEntity, DetailedAuthorViewDto>();

        CreateMap<AuthorFilterDto, AuthorFilter>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
