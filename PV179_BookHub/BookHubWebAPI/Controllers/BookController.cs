using BusinessLayer.DTOs.Book.Create;
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
    public async Task<IActionResult> UpdateBook(long id, CreateBookDto updateBookDto)
    {
        return Ok(
            await _bookFacade.UpdateBookAsync(id, updateBookDto)
            );
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        return Ok(
            await _bookFacade.FetchAllBooksAsync()
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        return Ok(
            await _bookFacade.FindBookByIdAsync(id)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        await _bookFacade.DeleteBookByIdAsync(id);
        return NoContent();
    }












    //[HttpPatch]
    //[Route("{bookId}/{authorId}")]
    //public async Task<IActionResult> AssignAuthorToBook(long bookId, long authorId)
    //{
    //    var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
    //    var author = await _unitOfWork.AuthorRepository.GetByIdAsync(authorId);

    //    if (book == null || author == null) 
    //    {
    //        return NotFound();
    //    }

    //    var authorBookAssociation = new AuthorBookAssociation()
    //    {
    //        AuthorId = authorId,
    //        BookId = bookId
    //    };

    //    await _unitOfWork.AuthorBookAssociationRepository.AddAsync(authorBookAssociation);

    //    return Ok(
    //        _mapper.Map<DetailedBookViewDto>(book)
    //        );
    //}







    //[HttpGet]
    //[Route("filter")]
    //public async Task<IActionResult> FetchBooksByFilters([FromQuery] IDictionary<string, string> query)
    //{
    //    var filter = new BookFilter(query);
    //    IQuery<Book> query1 = new QueryBase<Book, long>(_unitOfWork)
    //    {
    //        CurrentPage = 1,
    //        Filter = filter,
    //        SortAccordingTo = "Price",
    //        UseAscendingOrder = false
    //    };

    //    query1.Include(x => x.Authors, x => x.Reviews);
    //    query1.Where(filter.CreateExpression());
    //    query1.Page(1, 20);
    //    query1.SortBy("Price", false);

    //    var res = await query1.ExecuteAsync();
    //    var books = res.Items;

    //    return Ok(
    //        _mapper.Map<IEnumerable<DetailedBookViewDto>>(books)
    //        );
    //}
}
