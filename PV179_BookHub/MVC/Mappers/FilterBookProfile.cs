using AutoMapper;
using BusinessLayer.DTOs.Book.Filter;
using MVC.Models.Book;

namespace MVC.Mappers;

public class FilterBookProfile : Profile
{
    public FilterBookProfile()
    {
        CreateMap<SearchBooksModel, BookFilterDto>()
            .ForMember(x => x.SortParameter, 
                    opt => opt
                        .MapFrom(y => 
                            y.SortParameter == BookSortParam.None ? null : y.SortParameter.ToString())
                    );
    }
}
