using AutoMapper;
using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.Filter;
using BusinessLayer.DTOs.Publisher.View;
using Infrastructure.Query.Filters.EntityFilters;

namespace BusinessLayer.Mappers.Enitity;

public class PublisherProfile : Profile
{
    public PublisherProfile()
    {
        CreateMap<CreatePublisherDto, PublisherEntity>();
        CreateMap<PublisherEntity, GeneralPublisherViewDto>();
        CreateMap<PublisherEntity, DetailedPublisherViewDto>();

        CreateMap<PublisherFilterDto, PublisherFilter>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
