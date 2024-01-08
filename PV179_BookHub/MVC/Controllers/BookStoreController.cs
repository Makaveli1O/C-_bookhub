using BusinessLayer.Facades.BookStore;
using BusinessLayer.Facades.Address;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.BookStore.Create;

namespace MVC.Controllers;

public class BookStoreController : Controller
{
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly IAddressFacade _addressFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public BookStoreController(IBookStoreFacade bookStoreFacade, IAddressFacade addressFacade, UserManager<LocalIdentityUser> userManager)
    {
        _bookStoreFacade = bookStoreFacade;
        _addressFacade = addressFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var bookStores = await _bookStoreFacade.GetAllBookStores();
        return View(bookStores);
    }

    public async Task<IActionResult> Details(int id)
    {
        var bookStore = await _bookStoreFacade.GetBookStore(id);
        return View(bookStore);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookStoreDto createBookStoreDto)
    {
        await _bookStoreFacade.CreateBookStore(createBookStoreDto);
        ViewBag.Message = "Book Store Created Successfully";
        return View(createBookStoreDto);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var bookStoreDto = await _bookStoreFacade.GetBookStore(id);
        var addresses = await _addressFacade.GetAllAddressesAsync();
        ViewBag.Addresses = addresses.ToList();
        return View(bookStoreDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateBookStoreDto updateBookStoreDto)
    {
        var updated = await _bookStoreFacade.UpdateBookStore(id, updateBookStoreDto);
        ViewBag.Message = "Book Store Updated Successfully";
        return View(updated);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var bookStoreDto = await _bookStoreFacade.GetBookStore(id);
        return View(bookStoreDto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _bookStoreFacade.DeleteBookStore(id);
        return RedirectToAction(nameof(Index));
    }

}
