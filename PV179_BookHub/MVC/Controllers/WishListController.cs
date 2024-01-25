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
    private readonly UserManager<User> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IWishListFacade _wishListFacade;
    private readonly IBookFacade _bookFacade;

    public WishListController(
        UserManager<User> userManager,
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

    [HttpGet("{id:long}/Detail")]
    public async Task<JsonResult> Detail(long id)
    {
        var wishListItems = await _wishListFacade.FetchAllItemsFromWishListAsync(id);
        var wishList = await _wishListFacade.FetchWishListAsync(id);

        var model = new WishListDetailViewModel
        {
            Id = wishList.Id,
            UserId = wishList.UserId,
            CreatedAt = wishList.CreatedAt,
            Description = wishList.Description,
            Items = wishListItems
        };

        return Json(model, _jsonSerializerOptions);
    }

    [HttpGet("User/{userId:long}")]
    public async Task<JsonResult> SingleUserWishList(long userId)
    {
        return Json(await _wishListFacade.FetchAllByUserIdAsync(userId));
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
        wishList.UserId = user.Id;

        var wishListResult = await _wishListFacade.CreateWishListAsync(wishList);

        return RedirectToAction(nameof(Edit), new { id = wishListResult.Id });

    }

    [HttpGet("{id:long}/Edit")]
    public async Task<IActionResult> Edit(long id)
    {
        var wishList = await _wishListFacade.FetchWishListAsync(id);

        var wishListItems = await _wishListFacade.FetchAllItemsFromWishListAsync(id);

        var availableBooks = await _bookFacade.FetchAllBooksAsync();

        var viewModel = new WishListUpdateViewModel
        {
            Description = wishList.Description,
            WishListItems = wishListItems.Adapt<IList<WishListItemViewModel>>(),
            AvailableBooks = availableBooks.Adapt<IList<WishListAvailableBooksViewModel>>()
        };

        return View(viewModel);
    }


    [HttpPost("{id:long}/Edit")]
    public async Task<IActionResult> Edit(long id, WishListUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Model Error: {error.ErrorMessage}");
            }

            return BadRequest(ModelState);
        }

        if (!await IsUserWishListOwner(id))
        {
            return Unauthorized();
        }

        //update description
        await _wishListFacade.UpdateWishListAsync(id, model.Description);

        //remove books from wishlist
        foreach(var bookId in model.RemovedBooks)
        {
            await _wishListFacade.DeleteWishListItemAsync(bookId);
        }

        //add books to the wishlist
        foreach(var bookId in model.AddedBooks)
        {
            var createWishListItemDto = new CreateWishListItemDto
            {
                WishListId = id,
                BookId = bookId
            };

            await _wishListFacade.CreateWishListItemAsync(createWishListItemDto);
        }

        return RedirectToAction(nameof(Detail), new { id });
    }

    private async Task<bool> IsUserWishListOwner(long wishListId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return false;
        }

        var wishList = await _wishListFacade.FetchWishListAsync(wishListId);

        return wishList.UserId == user.Id;
    }
}
