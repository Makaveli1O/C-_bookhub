using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.Facades.WishList;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.WishList;
using System.Text.Json;
using BusinessLayer.Facades.Book;
using AutoMapper;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using MVC.Models.Base;
using BusinessLayer.DTOs.WishList.View;
using BusinessLayer.DTOs.WishList.Filter;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class WishListController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IWishListFacade _wishListFacade;
    private readonly IBookFacade _bookFacade;
    private readonly IMapper _mapper;

    public WishListController(
        UserManager<User> userManager,
        IWishListFacade wishListFacade,
        IBookFacade bookFacade,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _wishListFacade = wishListFacade;
        _bookFacade = bookFacade;
        _mapper = mapper;

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }

    public async Task<IActionResult> Index(WishListSearchModel searchModel)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var filterDto = _mapper.Map<WishListFilterDto>(searchModel);
        filterDto.UserId = user.Id;

        var result = await _wishListFacade.FetchFilteredWishListsAsync(filterDto);

        var viewModel = _mapper.Map<GenericFilteredModel<WishListSearchModel, GeneralWishListViewDto>>(result);
        viewModel.SearchModel = searchModel;

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateWishListViewModel createWishListViewModel)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var wishListDto = _mapper.Map<CreateWishListDto>(createWishListViewModel);
        wishListDto.UserId = user.Id;

        var newWishList = _wishListFacade.CreateWishListAsync(wishListDto);
        
        return RedirectToAction(nameof(Details), new { newWishList.Id, updated = true });
    }

    public async Task<IActionResult> Details(long id, bool updated)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var wishlist = await _wishListFacade.FetchWishListAsync(id);
        if (wishlist.UserId != user.Id)
        {
            return Unauthorized();
        }

        var items = await _wishListFacade.FetchAllItemsFromWishListAsync(id);

        if (updated)
        {
            ViewBag.Message = "WishList Saved Successfully";
        }

        var result = _mapper.Map<DetailsWishListModel>(wishlist);
        result.Items = items;
        result.OwnerName = user.Name;

        return View(result);
    }





    [HttpGet("User/{userId:long}")]
    public async Task<JsonResult> SingleUserWishList(long userId)
    {
        return Json(await _wishListFacade.FetchAllByUserIdAsync(userId));
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
            WishListItems = _mapper.Map<IList<WishListItemViewModel>>(wishListItems),
            AvailableBooks =_mapper.Map<IList<WishListAvailableBooksViewModel>>(availableBooks)
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

        return RedirectToAction(nameof(Details), new { id });
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
