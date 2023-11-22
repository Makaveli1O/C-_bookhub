using AutoMapper;
using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.View;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Mappers;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<CreatePublisherDto, Publisher>();
        CreateMap<Publisher, GeneralPublisherViewDto>();
    }
}
