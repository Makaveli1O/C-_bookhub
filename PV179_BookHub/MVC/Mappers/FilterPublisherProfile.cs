using AutoMapper;
using BusinessLayer.DTOs.BaseFilter;
using BusinessLayer.DTOs.Publisher.Filter;
using BusinessLayer.DTOs.Publisher.View;
using MVC.Models.Base;
using MVC.Models.Publisher;

namespace MVC.Mappers;

public class FilterPublisherProfile : Profile
{
    public FilterPublisherProfile()
    {
        CreateMap<PublisherSearchModel, PublisherFilterDto>()
            .ForMember(x => x.SortParameter,
                    opt => opt.MapFrom(y => y.SortParameter == PublisherSortParameters.None ? null : y.SortParameter.ToString()));

        CreateMap<FilterResultDto<GeneralPublisherViewDto>, GenericFilteredModel<PublisherSearchModel, GeneralPublisherViewDto>>();
    }
}
