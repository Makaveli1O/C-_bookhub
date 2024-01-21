using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Author;
using BusinessLayer.DTOs.Author.Create;

namespace MVC.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorFacade _authorFacade;
    private readonly UserManager<User> _userManager;

    public AuthorController(IAuthorFacade authorFacade, UserManager<User> userManager)
    {
        _authorFacade = authorFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _authorFacade.GetAllAuthorsAsync();
        return View(authors);
    }

    public async Task<IActionResult> Details(int id)
    {
        var author = await _authorFacade.FindAuthorByIdAsync(id);
        return View(author);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAuthorDto createAuthorDto)
    {
        await _authorFacade.CreateAuthorAsync(createAuthorDto);
        ViewBag.Message = "Author Created Successfully";
        return View(createAuthorDto);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var author = await _authorFacade.FindAuthorByIdAsync(id);
        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateAuthorDto updateAuthorDto)
    {
        var updated = await _authorFacade.UpdateAuthorAsync(id, updateAuthorDto);
        ViewBag.Message = "Author Updated Successfully";
        return View(updated);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var author = await _authorFacade.FindAuthorByIdAsync(id);
        return View(author);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _authorFacade.DeleteAuthorAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
