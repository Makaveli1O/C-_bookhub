using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.DTOs.Publisher.Create;

namespace MVC.Controllers;

public class PublisherController : Controller
{
    private readonly IPublisherFacade _publisherFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public PublisherController(IPublisherFacade publisherFacade, UserManager<LocalIdentityUser> userManager)
    {
        _publisherFacade = publisherFacade;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var publishers = await _publisherFacade.GetAllPublishersAsync();
        return View(publishers);
    }

    public async Task<IActionResult> Details(int id)
    {
        var publisher = await _publisherFacade.FindPublisherByIdAsync(id);
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
        await _publisherFacade.CreatePublisherAsync(createPublisherDto);
        ViewBag.Message = "Publisher Created Successfully";
        return View(createPublisherDto);
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
        ViewBag.Message = "Publisher Updated Successfully";
        return View(updated);
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
