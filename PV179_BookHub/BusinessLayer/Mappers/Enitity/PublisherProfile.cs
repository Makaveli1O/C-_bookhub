using AutoMapper;
using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.View;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Mappers.Enitity;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<CreatePublisherDto, PublisherEntity>();
        CreateMap<PublisherEntity, GeneralPublisherViewDto>();
        CreateMap<PublisherEntity, DetailedPublisherViewDto>();
    }
}
