using AutoMapper;
using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Mappers;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<Author, ViewAuthorDto>();
    }
}
