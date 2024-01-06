using AutoMapper;
using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.View;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Mappers.Enitity;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<CreateAuthorDto, AuthorEntity>();
        CreateMap<AuthorEntity, GeneralAuthorViewDto>();
        CreateMap<AuthorEntity, DetailedAuthorViewDto>();
    }
}
