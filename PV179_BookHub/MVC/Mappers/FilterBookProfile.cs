using AutoMapper;
using BusinessLayer.DTOs.BaseFilter;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.View;
using MVC.Models.Base;
using MVC.Models.Book;

namespace MVC.Mappers;

public class FilterBookProfile : Profile
{
    public FilterBookProfile()
    {
        CreateMap<BookSearchModel, BookFilterDto>()
            .ForMember(x => x.SortParameter,
                opt => opt.MapFrom(y => y.SortParameter == BookSortParam.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralBookViewDto>, GenericFilteredModel<BookSearchModel, GeneralBookViewDto>>();
    }
}
