using AutoMapper;
using BookHubWebAPI.Api.Book.Create;
using BookHubWebAPI.Api.Book.View;
using DataAccessLayer.Models.Publication;

namespace BookHubWebAPI.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, Book>();
        CreateMap<Book, GeneralBookViewDto>();
        CreateMap<Book, DetailedBookViewDto>();
    }
}
