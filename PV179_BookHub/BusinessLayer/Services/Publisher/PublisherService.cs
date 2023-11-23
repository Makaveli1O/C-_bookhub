using AutoMapper;
using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.View;
using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Publisher;

public class PublisherService : BaseService<DataAccessLayer.Models.Publication.Publisher, long>, IPublisherService
{
    public PublisherService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DetailedPublisherViewDto> CreatePublisherAsync(CreatePublisherDto createPublisherDto, bool save = true)
    {
        var publisher = _mapper.Map<DataAccessLayer.Models.Publication.Publisher>(createPublisherDto);
        await Repository.AddAsync(publisher);
        await SaveAsync(save);

        return _mapper.Map<DetailedPublisherViewDto>(publisher);

    }

    public async Task<DetailedPublisherViewDto> UpdatePublisherAsync(long id, CreatePublisherDto updatePublisherDto, bool save = true)
    {
        var publisher = await Repository.GetByIdAsync(id);
        if (publisher == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Publisher), id);
        }

        publisher.Name = updatePublisherDto.Name ?? publisher.Name;
        publisher.Country = updatePublisherDto.Country ?? publisher.Country;
        publisher.City = updatePublisherDto.City ?? publisher.City;
        publisher.YearFounded = updatePublisherDto.YearFounded ?? publisher.YearFounded;

        Repository.Update(publisher);
        await SaveAsync(save);

        return _mapper.Map<DetailedPublisherViewDto>(publisher);
    }
    
    public async Task<List<GeneralPublisherViewDto>> FetchAllPublishersAsync()
    {
        var publishers = await Repository.GetAllAsync();

        return _mapper.Map<List<GeneralPublisherViewDto>>(publishers);
    }
    
    public async Task<DetailedPublisherViewDto> FindPublisherByIdAsync(long id)
    {
        var publisher = await Repository
            .GetByIdAsync(
                id, 
                x => x.Books
                );

        if (publisher == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Publisher), id);
        }

        return _mapper.Map<DetailedPublisherViewDto>(publisher);
    }
    
    public async Task DeletePublisherByIdAsync(long id, bool save = true)
    {
        var publisher = await Repository.GetByIdAsync(id);
        if (publisher == null)
        {
            throw new NoSuchEntityException(typeof(DataAccessLayer.Models.Publication.Publisher), id);
        }

        Repository.Delete(publisher);
        await SaveAsync(save);
    }
}
