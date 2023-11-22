using AutoMapper;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Mappers;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, Book>();
        CreateMap<Book, GeneralBookViewDto>();
        CreateMap<Book, DetailedBookViewDto>();
    }
}
