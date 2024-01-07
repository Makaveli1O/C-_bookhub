using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.View;
using Infrastructure.Query.Filters.EntityFilters;

namespace BusinessLayer.Mappers.Enitity;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, BookEntity>();
        CreateMap<BookEntity, GeneralBookViewDto>();
        CreateMap<BookEntity, DetailedBookViewDto>();
        CreateMap<BookFilterDto, BookFilter>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
