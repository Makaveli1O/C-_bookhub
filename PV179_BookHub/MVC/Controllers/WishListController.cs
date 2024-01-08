using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using BusinessLayer.Facades.WishList;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.WishList;
using System.Text.Json;
using Mapster;
using BusinessLayer.Facades.Book;

namespace MVC.Controllers;

[Route("WishList")]
public class WishListController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IWishListFacade _wishListFacade;
    private readonly IBookFacade _bookFacade;

    public WishListController(
        UserManager<LocalIdentityUser> userManager,
        IWishListFacade wishListFacade,
        IBookFacade bookFacade
        )
    {
        _userManager = userManager;
        _wishListFacade = wishListFacade;
        _bookFacade = bookFacade;

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }

    [HttpGet("MyWishList")]
    public async Task<IActionResult> FetchMyWisList()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }


        return RedirectToAction(nameof(SingleUserWishList), new { id = user.UserId });
    }

    [HttpGet("{id:long}/WishList")]
    [Authorize]
    public async Task<JsonResult> SingleUserWishList(long id)
    {
        return Json(await _wishListFacade.FetchWishListAsync(id));
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost("Create")]
    public async Task<IActionResult> Create(WishListCreateViewModel model)
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

        var wishList = model.Adapt<CreateWishListDto>();
        wishList.UserId = user.UserId;

        var wishListResult = await _wishListFacade.CreateWishListAsync(wishList);

        return RedirectToAction(nameof(Edit), new { id = wishListResult.Id });

    }

    [HttpGet("Edit/{id:long}")]
    public async Task<IActionResult> Edit(long id)
    {   //tu potrebujem dostat WishListItems do modelu ktory sa posle do metody nizsie
        var wishList = await _wishListFacade.FetchWishListAsync(id);

        var wishListItems = await _wishListFacade.FetchAllItemsFromWishListAsync(id);

        var availableBooks = await _bookFacade.FetchAllBooksAsync();

        var viewModel = new WishListUpdateViewModel
        {
            Description = wishList.Description,
            WishListItems = wishListItems.Adapt<IEnumerable<WishListItemViewModel>>(),
            AvailableBooks = availableBooks.Adapt<IEnumerable<WishListAvailableBooksViewModel>>()
        };

        return View(viewModel);
    }


    [HttpPost("Edit/{id:long}")]
    public async Task<IActionResult> Edit(long id, WishListUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!await IsUserWishListOwner(id))
        {
            return Unauthorized();
        }

        var wishList = model.Adapt<GeneralWishListViewDto>();

        var wishListResult = await _wishListFacade.UpdateWishListAsync(wishList.Id, wishList.Description);

        return View(wishListResult);
    }

    private async Task<bool> IsUserWishListOwner(long wishListId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return false;
        }

        var wishList = await _wishListFacade.FetchWishListAsync(wishListId);

        return wishList.UserId == user.UserId;
    }
}
