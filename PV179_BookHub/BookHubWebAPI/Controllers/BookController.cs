using AutoMapper;
using BookHubWebAPI.Api.Book.Create;
using BookHubWebAPI.Api.Book.Filter;
using BookHubWebAPI.Api.Book.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Enums;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
    {
        var book = _mapper.Map<Book>(createBookDto);

        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.CommitAsync();

        return Created(
            new Uri($"{Request.Path}/{book.Id}", UriKind.Relative),
            _mapper.Map<DetailedBookViewDto>(book)
            );
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateBook(long id, CreateBookDto createBookDto)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book != null)
        {
            book.Title = createBookDto.Title ?? book.Title;
            book.ISBN = createBookDto.ISBN ?? book.ISBN;
            book.Author = createBookDto.Author ?? book.Author;
            book.Publisher = createBookDto.Publisher ?? book.Publisher;
            book.Description = createBookDto.Description ?? book.Description;
            book.BookGenre = createBookDto.BookGenre;
            book.Price = createBookDto.Price;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.CommitAsync();
        }

        return Ok(
            _mapper.Map<DetailedBookViewDto>(book)
            );
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        var books = await _unitOfWork.BookRepository.GetAllAsync();

        return Ok(
            _mapper.Map<List<GeneralBookViewDto>>(books)
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);

        return Ok(
            _mapper.Map<DetailedBookViewDto>(book)
            );
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> FetchBooksByFilters([FromQuery] IDictionary<string, string> query)
    {
        var filter = new BookFilter(query);
        var books = await _unitOfWork.BookRepository.GetAllFilteredAsync(filter.CreateEqualExpression());

        return Ok(
            _mapper.Map<IEnumerable<DetailedBookViewDto>>(books)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book != null)
        {
            _unitOfWork.BookRepository.Delete(book);
            await _unitOfWork.CommitAsync();
        }
        return NoContent();
    }
}