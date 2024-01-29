using AutoMapper;
using BusinessLayer.DTOs.Author.Filter;
using BusinessLayer.DTOs.Author.View;
using BusinessLayer.DTOs.BaseFilter;
using MVC.Models.Author;
using MVC.Models.Base;

namespace MVC.Mappers;

public class FilterAuthorProfile : Profile
{
    public FilterAuthorProfile()
    {
        CreateMap<AuthorSearchModel, AuthorFilterDto>()
            .ForMember(x => x.SortParameter,
                opt => opt.MapFrom(y => y.SortParameter == AuthorSortParameters.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralAuthorViewDto>, GenericFilteredModel<AuthorSearchModel, GeneralAuthorViewDto>>();
    }
}
