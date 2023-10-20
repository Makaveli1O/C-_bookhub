using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;

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
