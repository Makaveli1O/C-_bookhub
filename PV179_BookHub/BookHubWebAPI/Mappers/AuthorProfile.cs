using AutoMapper;
using BookHubWebAPI.Api.Author.Create;
using BookHubWebAPI.Api.Author.View;
using DataAccessLayer.Models.Publication;

namespace BookHubWebAPI.Mappers;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<CreateAuthorDto, Author>();
        CreateMap<Author, ViewAuthorDto>();
    }
}
