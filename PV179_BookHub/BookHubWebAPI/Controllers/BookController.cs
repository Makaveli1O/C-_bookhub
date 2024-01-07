using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Filter;
using BusinessLayer.Facades.Book;
using DataAccessLayer.Models.Enums;
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
    public async Task<IActionResult> UpdateBook(long id, CreateBookDto updateBookDto)
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
    [Route("{bookId}/{authorId}")]
    public async Task<IActionResult> AssignAuthorToBook(long bookId, long authorId)
    {
        return Ok(await _bookFacade.AssignAuthorToBook(bookId, authorId));
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> FetchBooksByFilters(string? title, string? author, string? publisher, string? description,
        BookGenre? bookGenre, double? LEQPrice, double? GEQPrice, string? sortParam, bool? asc, int pageNumber, int? pageSize)
    {
        var filter = new BookFilterDto(title, author, publisher, description, bookGenre, LEQPrice, GEQPrice, sortParam, asc, pageNumber, pageSize);

        var books = await _bookFacade.FetchFilteredBooksAsync(filter);
        return Ok(books);
    }
}
