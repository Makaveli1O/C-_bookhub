using BusinessLayer.Facades.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MVC.Controllers;

[Route("Book")]
public class BooksController : Controller
{
    private readonly IBookFacade _bookFacade;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public BooksController(IBookFacade bookFacade)
    {
        _bookFacade = bookFacade;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }
    public async Task<IActionResult> Index()
    {
        var books = await _bookFacade.FetchAllBooksAsync();
        return View(books);
    }

    [HttpGet("FetchAll")]
    [AllowAnonymous]
    public async Task<JsonResult> FetchAll()
    {
        return Json(await _bookFacade.FetchAllBooksAsync(), _jsonSerializerOptions);
    }

    [HttpGet("Details/{id}")]
    [AllowAnonymous]
    public async Task<JsonResult> ViewBookDetails(long id)
    {
        return Json(await _bookFacade.FindBookByIdAsync(id));
    }
}
