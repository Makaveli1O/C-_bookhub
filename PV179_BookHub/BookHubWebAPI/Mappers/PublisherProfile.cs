using AutoMapper;
using BookHubWebAPI.Api.Publisher.Create;
using BookHubWebAPI.Api.Publisher.View;
using DataAccessLayer.Models.Publication;

namespace BookHubWebAPI.Mappers;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<CreatePublisherDto, Publisher>();
        CreateMap<Publisher, ViewPublisherDto>();
    }
}
