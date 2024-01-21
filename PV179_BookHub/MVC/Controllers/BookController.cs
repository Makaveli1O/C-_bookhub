using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.Facades.Author;
using BusinessLayer.DTOs.Book.Create;
using BusinessLayer.DTOs.Book.Update;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers;
public class BookController : Controller
{
    private readonly IBookFacade _bookFacade;
    private readonly IPublisherFacade _publisherFacade;
    private readonly IAuthorFacade _authorFacade;
    private readonly UserManager<User> _userManager;

    public BookController(IBookFacade bookFacade, IAuthorFacade authorFacade, IPublisherFacade publisherFacade, UserManager<User> userManager)
    {
        _bookFacade = bookFacade;
        _publisherFacade = publisherFacade;
        _authorFacade = authorFacade;
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

    public async Task<IActionResult> Create()
    {
        var publishers = await _publisherFacade.GetAllPublishersAsync();
        ViewBag.Publishers = new SelectList((await _publisherFacade.GetAllPublishersAsync()).ToList(), "Id", "Name");
        ViewBag.Authors = new SelectList((await _authorFacade.GetAllAuthorsAsync()).ToList(), "Id", "Name");
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
    public async Task<IActionResult> Edit(int id, UpdateBookDto updateBookDto)
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
