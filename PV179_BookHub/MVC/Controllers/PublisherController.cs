using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.DTOs.Publisher.Create;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class PublisherController : Controller
{
    private readonly IPublisherFacade _publisherFacade;
    private readonly UserManager<User> _userManager;

    public PublisherController(IPublisherFacade publisherFacade, UserManager<User> userManager)
    {
        _publisherFacade = publisherFacade;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var publishers = await _publisherFacade.GetAllPublishersAsync();
        return View(publishers);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id, bool updated)
    {
        var publisher = await _publisherFacade.FindPublisherByIdAsync(id);
        if (updated)
        {
            ViewBag.Message = "Publisher Saved Successfully";
        }
        return View(publisher);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePublisherDto createPublisherDto)
    {
        var created = await _publisherFacade.CreatePublisherAsync(createPublisherDto);
        return RedirectToAction(nameof(Details), new { created.Id, updated = true });
    }


    public async Task<IActionResult> Edit(int id)
    {
        var publisher = await _publisherFacade.FindPublisherByIdAsync(id);
        return View(publisher);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreatePublisherDto updatePublisherDto)
    {
        var updated = await _publisherFacade.UpdatePublisherAsync(id, updatePublisherDto);
        return RedirectToAction(nameof(Details), new { id, updated = true });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var address = await _publisherFacade.FindPublisherByIdAsync(id);
        return View(address);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _publisherFacade.DeletePublisherAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
