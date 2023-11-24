using AutoMapper;
using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.View;
using BusinessLayer.Services;

namespace BusinessLayer.Facades.Publisher
{
    public class PublisherFacade : BaseFacade, IPublisherFacade
    {
        private readonly IGenericService<DataAccessLayer.Models.Publication.Publisher, long> _publisherService;

        public PublisherFacade(IMapper mapper, IGenericService<DataAccessLayer.Models.Publication.Publisher, long> publisherService) : base(mapper)
        {
            _publisherService = publisherService;
        }

        public async Task<DetailedPublisherViewDto> CreatePublisherAsync(CreatePublisherDto createPublisherDto)
        {
            var publisher = _mapper.Map<DataAccessLayer.Models.Publication.Publisher>(createPublisherDto);
            await _publisherService.CreateAsync(publisher);

            return _mapper.Map<DetailedPublisherViewDto>(publisher);
        }

        public async Task DeletePublisherAsync(long id)
        {
            var publisher = await _publisherService.FindByIdAsync(id);

            await _publisherService.DeleteAsync(publisher);
        }

        public async Task<DetailedPublisherViewDto> FindPublisherByIdAsync(long id)
        {
            var publisher = await _publisherService.FindByIdAsync(id);

            return _mapper.Map<DetailedPublisherViewDto>(publisher);
        }

        public async Task<IEnumerable<GeneralPublisherViewDto>> GetAllPublishersAsync()
        {
            return _mapper.Map<List<GeneralPublisherViewDto>>(await _publisherService.FetchAllAsync());
        }

        public async Task<DetailedPublisherViewDto> UpdatePublisherAsync(long id, CreatePublisherDto updatePublisherDto)
        {
            var publisher = await _publisherService.FindByIdAsync(id);

            publisher.Name = updatePublisherDto.Name ?? publisher.Name;
            publisher.Country = updatePublisherDto.Country ?? publisher.Country;
            publisher.City = updatePublisherDto.City ?? publisher.City;
            publisher.YearFounded = updatePublisherDto.YearFounded ?? publisher.YearFounded;

            await _publisherService.UpdateAsync(publisher);
            return _mapper.Map<DetailedPublisherViewDto>(publisher);
        }
    }
}
