using BusinessLayer.Facades.Book;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class BooksController : Controller
{
    private readonly IBookFacade _bookFacade;

    public BooksController(IBookFacade bookFacade)
    {
        _bookFacade = bookFacade;
    }
    public async Task<IActionResult> Index()
    {
        var books = await _bookFacade.FetchAllBooksAsync();
        return View(books);
    }
}
