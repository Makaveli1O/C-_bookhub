using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

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

        await _unitOfWork.BookRepository.Add(book);
        _unitOfWork.Commit();

        return Ok(_mapper.Map<DetailedBookViewDto>(book));            // Should return created
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

            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.Commit();
        }

        return Ok(book);
    }

    [HttpGet]
    public async Task<IActionResult> FetchAll()
    {
        var books = await _unitOfWork.BookRepository.GetAll();

        return Ok(
            _mapper.Map<List<GeneralBookViewDto>>(books)
            );
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FetchSingle(long id)
    {
        var book = await _unitOfWork.BookRepository.GetById(id);

        return Ok(
            _mapper.Map<DetailedBookViewDto>(book)
            );
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteById(long id)
    {
        var book = await _unitOfWork.BookRepository.GetById(id);
        if (book != null)
        {
            _unitOfWork.BookRepository.Delete(book);
            _unitOfWork.Commit();
        }
        return NoContent();
    }
}