using BookHubWebAPI.Api.Create;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BookController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
    {

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        var books = await _unitOfWork.BookRepository.GetAll();
        return Ok(books);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var book = await _unitOfWork.BookRepository.GetById(id);
        return Ok(book);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var book = await _unitOfWork.BookRepository.GetById(id);
        if (book != null)
        {
            _unitOfWork.BookRepository.Delete(book);
        }
        return NoContent();
    }
}