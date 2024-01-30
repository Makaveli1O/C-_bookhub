using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.Facades.WishList;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.WishList;
using BusinessLayer.Facades.Book;
using AutoMapper;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using MVC.Models.Base;
using BusinessLayer.DTOs.WishList.View;
using BusinessLayer.DTOs.WishList.Filter;
using DataAccessLayer.Models.Preferences;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.User)]
public class WishListController : Controller
{
    private readonly UserManager<User> _userManager;
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

        var newWishList = await _wishListFacade.CreateWishListAsync(wishListDto);
        
        return RedirectToAction(nameof(Details), new { newWishList.Id, updated = true });
    }

    public async Task<IActionResult> Details(long id, bool updated)
    {
        var user = await _userManager.GetUserAsync(User);
        var wishlist = await _wishListFacade.FetchWishListAsync(id);
        if (user == null || wishlist.UserId != user.Id)
        {
            return Unauthorized();
        }

        if (updated)
        {
            ViewBag.Message = "WishList Saved Successfully";
        }

        var result = _mapper.Map<DetailsWishListModel>(wishlist);
        result.Items = await _wishListFacade.FetchAllItemsFromWishListAsync(id);
        result.OwnerName = user.Name;

        return View(result);
    }

    public async Task<IActionResult> Edit(long id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var wishlist = await _wishListFacade.FetchWishListAsync(id);
        var result = _mapper.Map<UpdateWishListViewModel>(wishlist);
        result.OwnerName = user.Name;
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, UpdateWishListViewModel updateWishListViewModel)
    {
        var user = await _userManager.GetUserAsync(User);
        var wishList = await _wishListFacade.FetchWishListAsync(id);
        if (user == null || wishList.UserId != user.Id)
        {
            return Unauthorized();
        }

        var updated = await _wishListFacade.UpdateWishListAsync(id, updateWishListViewModel.Description);
        return RedirectToAction(nameof(Details), new { id, updated = true });
    }

    public async Task<IActionResult> Delete(long id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var wishlist = await _wishListFacade.FetchWishListAsync(id);
        var result = _mapper.Map<DetailsWishListModel>(wishlist);
        result.Items = await _wishListFacade.FetchAllItemsFromWishListAsync(id);
        result.OwnerName = user.Name;
        return View(result);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var user = await _userManager.GetUserAsync(User);
        var wishlist = await _wishListFacade.FetchWishListAsync(id);
        if (user == null || wishlist.UserId != user.Id)
        {
            return Unauthorized();
        }

        await _wishListFacade.DeleteWishListAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CreateItem(long bookId, string bookTitle, string bookISBN)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        ViewBag.WishLists = new SelectList((await _wishListFacade.FetchAllByUserIdAsync(user.Id)).ToList(), "Id", "Description");
        var result = new CreateWishListItemViewModel()
        {
            BookId = bookId,
            BookTitle = bookTitle,
            BookISBN = bookISBN
        };
        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateItem(CreateWishListItemViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        var wishList = await _wishListFacade.FetchWishListAsync(model.WishListId);
        if (user == null || wishList.UserId != user.Id)
        {
            return Unauthorized();
        }

        var newItem = await _wishListFacade.CreateWishListItemAsync(_mapper.Map<CreateWishListItemDto>(model));

        return RedirectToAction(nameof(ItemDetails), new { id = newItem.Id, updated = true });
    }

    public async Task<IActionResult> ItemDetails(long id, bool updated)
    {
        var item = await _wishListFacade.FetchSingleItemFromWishListAsync(id);
        var wishList = await _wishListFacade.FetchWishListAsync(item.WishListId);
        var user = await _userManager.GetUserAsync(User);
        if (user == null || wishList.UserId != user.Id)
        {
            return Unauthorized();
        }

        if (updated)
        {
            ViewBag.Message = "WishList Item Saved Successfully";
        }

        var result = _mapper.Map<DetailsWishListItemModel>(item);

        return View(result);
    }
}
