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
    [Route("create/{id}")]
    public async Task<IActionResult> Create(int id)
    {
        Book book = new Book()
        {
            Title = $"Test {id}",
            Description = $"Test {id}"
        };

        await _unitOfWork.BookRepository.Add(book);
        _unitOfWork.Commit();

        return Ok(book);
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> ListAll()
    {
        var books = await _unitOfWork.BookRepository.GetAll();
        return Ok(books.Select(book => new { Id = book.Id, Title = book.Title, Desc = book.Description }));
    }
}