using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.BookReview;
using BusinessLayer.Facades.Order;
using BusinessLayer.Facades.User;
using BusinessLayer.Facades.WishList;
using DataAccessLayer.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.WishList;
using System.Text.Json;

namespace MVC.Controllers;


[Route("User")]
public class UserController : Controller
{
    private readonly IBookFacade _bookFacade;
    private readonly IWishListFacade _wishListFacade;
    private readonly IOrderFacade _orderFacade;
    private readonly IUserFacade _userFacade;
    private readonly IBookReviewFacade _bookReviewFacade;
    private readonly UserManager<LocalIdentityUser> _userManager;

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public UserController(
        UserManager<LocalIdentityUser> userManager,
        IBookFacade bookFacade,
        IWishListFacade wishListFacade,
        IOrderFacade orderFacade,
        IUserFacade userFacade,
        IBookReviewFacade bookReviewFacade
        )
    {
        _userManager = userManager;
        _bookFacade = bookFacade;
        _wishListFacade = wishListFacade;
        _orderFacade = orderFacade;
        _userFacade = userFacade;
        _bookReviewFacade = bookReviewFacade;

        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }
    
    public ActionResult Index()
    {
        return View();
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

    [HttpGet("WishListCreate")]
    public IActionResult WishListCreate()
    {
        return View();
    }


    [HttpPost("WishListCreate")]
    public async Task<IActionResult> WishListCreate(WishListCreateViewModel model)
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

        return RedirectToAction(nameof(WishListEdit), new { id = wishListResult.Id });

    }

    [HttpGet("WishListEdit/{id:long}")]
    public async Task<IActionResult> WishListEdit(long id)
    {
        var wishList = await _wishListFacade.FetchWishListAsync(id);

        return View(wishList.Adapt<WishListUpdateViewModel>());
    }


    [HttpPost("WishListEdit/{id:long}")]
    public async Task<IActionResult> WishListEdit(long id, WishListUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!await IsUserWishListOwner(id))
        {
            return BadRequest();
        }

        var wishList = model.Adapt<GeneralWishListViewDto>();

        var wishListResult = await _wishListFacade.UpdateWishListAsync(wishList.Id, wishList.Description);
        //TODO: dropdown for available books to add to wishlist


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

    [HttpGet("MyOrders")]
    public async Task<IActionResult> FetchMyOrders()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(SingleUserOrders), new {id = user.UserId });
    }

    [HttpGet("{id:long}/Orders")]
    [Authorize]
    public async Task<JsonResult> SingleUserOrders(long id)
    {
        return Json(await _orderFacade.FetchOrdersByUserIdAsync(id));
    }

    [HttpGet("MyReviews")]
    public async Task<IActionResult> FetchMyBookReviews()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(SingleUserBookReviews), new { id = user.UserId });
    }

    [HttpGet("{id:long}/BookReviews")]
    [Authorize]
    public async Task<JsonResult> SingleUserBookReviews(long id)
    {
        return Json(await _bookReviewFacade.FindUserReviewsAsync(id));
    }
}