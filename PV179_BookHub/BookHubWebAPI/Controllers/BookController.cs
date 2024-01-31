using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.DTOs.Book.Update;
using BusinessLayer.Facades.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookFacade _bookFacade;

    public BookController(IBookFacade bookFacade)
    {
        _bookFacade = bookFacade;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
    {
        var newBook = await _bookFacade.CreateBookAsync(createBookDto);
        return Created(
            new Uri($"{Request.Path}/{newBook.Id}", UriKind.Relative),
            newBook
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateBook(long id, UpdateBookDto updateBookDto)
    {
        return Ok(await _bookFacade.UpdateBookAsync(id, updateBookDto));
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        return Ok(await _bookFacade.FetchAllBooksAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        return Ok(await _bookFacade.FindBookByIdAsync(id));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        await _bookFacade.DeleteBookByIdAsync(id);
        return NoContent();
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> AssignAuthorToBook(long id, AuthorBookAssociationDto authorBookAssociation, [FromQuery] bool force = false)
    {
        return Ok(await _bookFacade.AssignAuthorToBookAsync(id, authorBookAssociation, force));
    }

    [HttpDelete]
    [Route("{bookId}/{authorId}")]
    public async Task<IActionResult> UnassignAuthorFromBook(long bookId, long authorId)
    {
        await _bookFacade.UnassignAuthorFromBookAsync(bookId, authorId);
        return NoContent();
    }

    [HttpPatch]
    [Route("primary/{id}")]
    public async Task<IActionResult> MakeUnmakeAuthorPrimary(long id, AuthorBookAssociationDto authorBookAssociation, [FromQuery] bool force = false)
    {
        return Ok(await _bookFacade.MakeUnmakeAuthorPrimaryAsync(id, authorBookAssociation, force));
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> FetchBooksByFilters([FromQuery] BookFilterDto bookFilterDto)
    {
        var books = await _bookFacade.FetchFilteredBooksAsync(bookFilterDto);
        return Ok(books);
    }
}
