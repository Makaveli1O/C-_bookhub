using BookHubWebAPI.Api.Create;
using DataAccessLayer.Models;
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
        // TODO: Use mapper
        var book = new Book()
        {
            Title = createBookDto.Title,
            Author = createBookDto.Author,
            Publisher = createBookDto.Publisher,
            Description = createBookDto.Description,
            BookGenre = createBookDto.BookGenre,
            Price = createBookDto.Price,
        };

        await _unitOfWork.BookRepository.Add(book);
        return Ok(book);            // Should return created
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateBook(long id, CreateBookDto createBookDto)
    {
        var book = await _unitOfWork.BookRepository.GetById(id);
        if (book != null)
        {
            book.Title = createBookDto.Title ?? book.Title;
            book.Author = createBookDto.Author ?? book.Author;
            book.Publisher = createBookDto.Publisher ?? book.Publisher;
            book.Description = createBookDto.Description ?? book.Description;
            book.BookGenre = createBookDto.BookGenre;
            book.Price = createBookDto.Price;
        }

        return Ok(book);
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