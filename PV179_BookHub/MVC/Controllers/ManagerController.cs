using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.Facades.BookStore;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.InventoryItem;
using Mapster;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers;

[Route("Manager")]
//[Authorize(Roles = "Manager")]
public class ManagerController : Controller
{
    private readonly IInventoryItemFacade _inventoryItemFacade;
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    public ManagerController(IInventoryItemFacade inventoryItemFacade,
                             IBookStoreFacade bookStoreFacade,
                             UserManager<LocalIdentityUser> userManager)
    {
        _inventoryItemFacade = inventoryItemFacade;
        _bookStoreFacade = bookStoreFacade;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        return RedirectToAction(nameof(GetAllInventoryItems));
    }

    [HttpGet]
    [Route("InventoryItems")]
    public async Task<IActionResult> GetAllInventoryItems()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        return View(await _inventoryItemFacade.GetAllInventoryItemsByUserId(user.User.Id)); // or UserID
    }

    [HttpGet]
    [Route("InventoryItem/{id}")]
    public async Task<IActionResult> GetInventoryItem(long id)
    {
        return View(await _inventoryItemFacade.GetInventoryItem(id));
    }
    
    [HttpGet]
    [Route("NewInventoryItem")]
    public async Task<IActionResult> CreateInventoryItem()
    {
        return View();
    }

    [HttpPost]
    [Route("NewInventoryItem")]
    public async Task<IActionResult> CreateInventoryItem(InventoryItemCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var bookStore = await _bookStoreFacade.GetBookStoreByUserId(user.User.Id); // or UserID
        model.BookStoreId = bookStore.Id;
        
        var createdInventoryItem = await _inventoryItemFacade.CreateInventoryItem(model.Adapt<CreateInventoryItemDto>());

        return View(createdInventoryItem.Adapt<InventoryItemCreateViewModel>());
    }

    [HttpGet]
    [Route("InventoryItemUpdate/{id}")]
    public async Task<IActionResult> UpdateInventoryItem(long id)
    {
        var inventoryItems = await _inventoryItemFacade.GetInventoryItem(id);
        return View(inventoryItems.Adapt<InventoryItemCreateViewModel>());
    }

    [HttpPost]
    [Route("InventoryItemUpdate/{id}")]
    public async Task<IActionResult> UpdateInventoryItem(long id, InventoryItemCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedInventoryItem = await _inventoryItemFacade.UpdateInventoryItem(id, model.Adapt<CreateInventoryItemDto>());
        
        return View(updatedInventoryItem.Adapt<InventoryItemCreateViewModel>());
    }

    [HttpPost]
    [Route("InventoryDelete/{id}")]
    public async Task<IActionResult> DeleteInventoryItem(long id)
    {
        await _inventoryItemFacade.DeleteInventoryItem(id);
        return RedirectToAction(nameof(GetAllInventoryItems));
    }
}
