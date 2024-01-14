using BusinessLayer.Facades.BookStore;
using BusinessLayer.Facades.Address;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.BookStore.Create;
using DataAccessLayer.Models.Enums;
using BusinessLayer.DTOs.Address.View;
using BusinessLayer.DTOs.User.View;
using AutoMapper;

namespace MVC.Controllers;

public class BookStoreController : Controller
{
    private readonly IMapper _mapper;
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly IAddressFacade _addressFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public BookStoreController(IMapper mapper, IBookStoreFacade bookStoreFacade, IAddressFacade addressFacade, UserManager<LocalIdentityUser> userManager)
    {
        _mapper = mapper;
        _bookStoreFacade = bookStoreFacade;
        _addressFacade = addressFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var bookStores = await _bookStoreFacade.GetAllBookStores();
        return View(bookStores);
    }

    public async Task<IActionResult> Details(int id, bool updatedFlag)
    {
        var bookStore = await _bookStoreFacade.GetBookStore(id);
        if (updatedFlag)
        {
            ViewBag.Message = "Book Store Saved Successfully";
        }
        return View(bookStore);
    }

    public async Task<IActionResult> Create()
    {
        var addresses = await _addressFacade.GetAvailableAddressesForBookStoreAsync(null);
        var managers = await _userManager.GetUsersInRoleAsync(UserRole.Manager.ToString());
        var availableManagers = _mapper.Map<IEnumerable<GeneralUserViewDto>>(
            managers.Where(u => u.User?.BookStore == null)); // possible optimization?? delegate to layer bellow??

        ViewBag.Addresses = addresses.ToList();
        ViewBag.Managers = availableManagers.ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookStoreDto createBookStoreDto)
    {
        var created  = await _bookStoreFacade.CreateBookStore(createBookStoreDto);
        return RedirectToAction(nameof(Details), new { created.Id, updatedFlag = true });
    }


    public async Task<IActionResult> Edit(int id)
    {
        var bookStoreDto = await _bookStoreFacade.GetBookStore(id);

        var addresses = await _addressFacade.GetAvailableAddressesForBookStoreAsync(id);
        var managers = await _userManager.GetUsersInRoleAsync(UserRole.Manager.ToString());
        var availableManagers = _mapper.Map<IEnumerable<GeneralUserViewDto>>(
            managers.Where(u => u.User?.BookStore == null || u.User.BookStore.Id == id)); // possible optimization?? delegate to layer bellow??

        ViewBag.Addresses = addresses.ToList();
        ViewBag.Managers = availableManagers.ToList();
        return View(_mapper.Map<CreateBookStoreDto>(bookStoreDto));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateBookStoreDto updateBookStoreDto)
    {
        var updated = await _bookStoreFacade.UpdateBookStore(id, updateBookStoreDto);
        return RedirectToAction(nameof(Details), new { id, updatedFlag = true });
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
