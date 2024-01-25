using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Author;
using BusinessLayer.DTOs.Author.Create;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class AuthorController : Controller
{
    private readonly IAuthorFacade _authorFacade;
    private readonly UserManager<User> _userManager;

    public AuthorController(IAuthorFacade authorFacade, UserManager<User> userManager)
    {
        _authorFacade = authorFacade;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var authors = await _authorFacade.GetAllAuthorsAsync();
        return View(authors);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id, bool updated)
    {
        var author = await _authorFacade.FindAuthorByIdAsync(id);
        if (updated)
        {
            ViewBag.Message = "Author Saved Successfully";
        }
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
        var created = await _authorFacade.CreateAuthorAsync(createAuthorDto);
        return RedirectToAction(nameof(Details), new { created.Id, updated = true });
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
        return RedirectToAction(nameof(Details), new { id, updated = true });
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
