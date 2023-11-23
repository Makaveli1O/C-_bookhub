using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.View;

namespace BusinessLayer.Services.Publisher;

public interface IPublisherService : IBaseService
{
    Task<DetailedPublisherViewDto> CreatePublisherAsync(CreatePublisherDto createPublisherDto, bool save = true);
    Task<DetailedPublisherViewDto> UpdatePublisherAsync(long id, CreatePublisherDto updatePublisherDto, bool save = true);
    Task<List<GeneralPublisherViewDto>> FetchAllPublishersAsync();
    Task<DetailedPublisherViewDto> FindPublisherByIdAsync(long id);
    Task DeletePublisherByIdAsync(long id, bool save = true);
}
