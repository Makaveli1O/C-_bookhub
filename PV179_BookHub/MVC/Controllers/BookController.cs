using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Book;
using BusinessLayer.DTOs.Book.Create;

namespace MVC.Controllers;

public class BookController : Controller
{
    private readonly IBookFacade _bookFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public BookController(IBookFacade bookFacade, UserManager<LocalIdentityUser> userManager)
    {
        _bookFacade = bookFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookFacade.FetchAllBooksAsync();
        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookFacade.FindBookByIdAsync(id);
        return View(book);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookDto createBookDto)
    {
        await _bookFacade.CreateBookAsync(createBookDto);
        ViewBag.Message = "Book Created Successfully";
        return View(createBookDto);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookFacade.FindBookByIdAsync(id);
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateBookDto updateBookDto)
    {
        var updated = await _bookFacade.UpdateBookAsync(id, updateBookDto);
        ViewBag.Message = "Book Updated Successfully";
        return View(updated);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookFacade.FindBookByIdAsync(id);
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _bookFacade.DeleteBookByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
