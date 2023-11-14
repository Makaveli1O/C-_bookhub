using AutoMapper;
using BookHubWebAPI.Api.Publisher.Create;
using BookHubWebAPI.Api.Publisher.View;
using DataAccessLayer.Models.Publication;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PublisherController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PublisherController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisher(CreatePublisherDto createPublisherDto)
    {
        var publisher = _mapper.Map<Publisher>(createPublisherDto);

        await _unitOfWork.PublisherRepository.AddAsync(publisher);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{publisher.Id}", UriKind.Relative),
            _mapper.Map<ViewPublisherDto>(publisher)
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdatePublisher(long id, CreatePublisherDto createPublisherDto)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        publisher.Name = createPublisherDto.Name ?? publisher.Name;

        _unitOfWork.PublisherRepository.Update(publisher);
        await _unitOfWork.CommitAsync();

        return Ok(
            _mapper.Map<ViewPublisherDto>(publisher)
            );
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        var publishers = await _unitOfWork.PublisherRepository.GetAllAsync();

        return Ok(
            _mapper.Map<List<ViewPublisherDto>>(publishers)
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(
            _mapper.Map<ViewPublisherDto>(publisher)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        _unitOfWork.PublisherRepository.Delete(publisher);
        await _unitOfWork.CommitAsync();
        return NoContent();
    }
}
