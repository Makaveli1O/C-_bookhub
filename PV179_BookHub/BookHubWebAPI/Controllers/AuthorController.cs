using BusinessLayer.DTOs.Author.Create;
using BusinessLayer.DTOs.Author.Filter;
using BusinessLayer.Facades.Author;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorFacade _authorFacade;

    public AuthorController(IAuthorFacade authorFacade)
    {
        _authorFacade = authorFacade;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = await _authorFacade.CreateAuthorAsync(createAuthorDto);

        return Created(
            new Uri($"{Request.Path}/{author.Id}", UriKind.Relative),
            author
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAuthor(long id, CreateAuthorDto updateAuthorDto)
    {
        return Ok(await _authorFacade.UpdateAuthorAsync(id, updateAuthorDto));
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        return Ok(await _authorFacade.GetAllAuthorsAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        return Ok(await _authorFacade.FindAuthorByIdAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        await _authorFacade.DeleteAuthorAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> FetchAuthorsByFilters([FromQuery] AuthorFilterDto authorFilterDto)
    {
        var authors = await _authorFacade.FetchFilteredAuthorsAsync(authorFilterDto);
        return Ok(authors);
    }
}
