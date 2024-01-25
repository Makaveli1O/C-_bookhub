using BusinessLayer.DTOs.BaseFilter;
using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.Filter;
using BusinessLayer.DTOs.Publisher.View;

namespace BusinessLayer.Facades.Publisher;

public interface IPublisherFacade
{
    Task<DetailedPublisherViewDto> CreatePublisherAsync(CreatePublisherDto createPublisherDto);
    Task<DetailedPublisherViewDto> UpdatePublisherAsync(long id, CreatePublisherDto updatePublisherDto);
    Task<DetailedPublisherViewDto> FindPublisherByIdAsync(long id);
    Task<IEnumerable<GeneralPublisherViewDto>> GetAllPublishersAsync();
    Task<FilterResultDto<GeneralPublisherViewDto>> FetchFilteredPublishersAsync(PublisherFilterDto publisherFilterDto);
    Task DeletePublisherAsync(long id);
}
