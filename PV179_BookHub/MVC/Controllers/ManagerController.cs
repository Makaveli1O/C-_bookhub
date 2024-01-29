using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.Facades.BookStore;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.InventoryItem;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Models.Enums;
using AutoMapper;

namespace MVC.Controllers;

[Route("Manager")]
[Authorize(Roles = UserRoles.Manager)]
public class ManagerController : Controller
{
    private readonly IInventoryItemFacade _inventoryItemFacade;
    private readonly IBookStoreFacade _bookStoreFacade;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public ManagerController(IInventoryItemFacade inventoryItemFacade,
                             IBookStoreFacade bookStoreFacade,
                             UserManager<User> userManager,
                             IMapper mapper)
    {
        _inventoryItemFacade = inventoryItemFacade;
        _bookStoreFacade = bookStoreFacade;
        _userManager = userManager;
        _mapper = mapper;
    }
    public IActionResult Index()
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

        return View(await _inventoryItemFacade.GetAllInventoryItemsByUserId(user.Id)); // or UserID
    }

    [HttpGet]
    [Route("InventoryItem/{id}")]
    public async Task<IActionResult> GetInventoryItem(long id)
    {
        return View(await _inventoryItemFacade.GetInventoryItem(id));
    }
    
    [HttpGet]
    [Route("NewInventoryItem")]
    public IActionResult CreateInventoryItem()
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

        var bookStore = await _bookStoreFacade.GetBookStoreByUserId(user.Id); // or UserID
        model.BookStoreId = bookStore.Id;
        
        var createdInventoryItem = await _inventoryItemFacade.CreateInventoryItem(_mapper.Map<CreateInventoryItemDto>(model));

        return View(_mapper.Map<InventoryItemCreateViewModel>(createdInventoryItem));
    }

    [HttpGet]
    [Route("InventoryItemUpdate/{id}")]
    public async Task<IActionResult> UpdateInventoryItem(long id)
    {
        var inventoryItems = await _inventoryItemFacade.GetInventoryItem(id);
        return View(_mapper.Map<InventoryItemCreateViewModel>(inventoryItems));
    }

    [HttpPost]
    [Route("InventoryItemUpdate/{id}")]
    public async Task<IActionResult> UpdateInventoryItem(long id, InventoryItemCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedInventoryItem = await _inventoryItemFacade.UpdateInventoryItem(id, _mapper.Map<CreateInventoryItemDto>(model));
        
        return View(_mapper.Map<InventoryItemCreateViewModel>(updatedInventoryItem));
    }

    [HttpPost]
    [Route("InventoryDelete/{id}")]
    public async Task<IActionResult> DeleteInventoryItem(long id)
    {
        await _inventoryItemFacade.DeleteInventoryItem(id);
        return RedirectToAction(nameof(GetAllInventoryItems));
    }
}
