using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Author;
using BusinessLayer.DTOs.Author.Create;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using MVC.Models.Author;
using AutoMapper;
using BusinessLayer.DTOs.Author.Filter;
using MVC.Models.Base;
using BusinessLayer.DTOs.Author.View;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class AuthorController : Controller
{
    private readonly IAuthorFacade _authorFacade;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorFacade authorFacade, UserManager<User> userManager, IMapper mapper)
    {
        _authorFacade = authorFacade;
        _userManager = userManager;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index(AuthorSearchModel authorSearchModel)
    {
        var result = await _authorFacade
            .FetchFilteredAuthorsAsync(
                _mapper.Map<AuthorFilterDto>(authorSearchModel)
            );

        var viewModel = _mapper.Map<GenericFilteredModel<AuthorSearchModel, GeneralAuthorViewDto>>(result);
        viewModel.SearchModel = authorSearchModel;

        return View(viewModel);
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
