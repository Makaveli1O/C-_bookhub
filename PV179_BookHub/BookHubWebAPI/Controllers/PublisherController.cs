using BusinessLayer.DTOs.Publisher.Create;
using BusinessLayer.DTOs.Publisher.Filter;
using BusinessLayer.Facades.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PublisherController : ControllerBase
{
    private readonly IPublisherFacade _publisherFacade;

    public PublisherController(IPublisherFacade publisherFacade)
    {
        _publisherFacade = publisherFacade;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePublisher(CreatePublisherDto createPublisherDto)
    {
        var publisher = await _publisherFacade.CreatePublisherAsync(createPublisherDto);

        return Created(
            new Uri($"{Request.Path}/{publisher.Id}", UriKind.Relative),
            publisher
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdatePublisher(long id, CreatePublisherDto createPublisherDto)
    {
        return Ok(await _publisherFacade.UpdatePublisherAsync(id, createPublisherDto));
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        return Ok(await _publisherFacade.GetAllPublishersAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        return Ok(await _publisherFacade.FindPublisherByIdAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        await _publisherFacade.DeletePublisherAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> FetchPublishersByFilters([FromQuery] PublisherFilterDto publisherFilterDto)
    {
        var publishers = await _publisherFacade.FetchFilteredPublishersAsync(publisherFilterDto);
        return Ok(publishers);
    }
}
