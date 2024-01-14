using BusinessLayer.Facades.User;
using DataAccessLayer.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models.User;

namespace MVC.Controllers;

[Route("User")]
public class UserController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly IUserFacade _userFacade;

    public UserController(UserManager<LocalIdentityUser> userManager, IUserFacade userFacade)
    {
        _userManager = userManager;
        _userFacade = userFacade;
    }
    
    public IActionResult Index()
    {
        return RedirectToAction(nameof(Users));
    }

    [HttpGet("MyProfile")]
    public async Task<IActionResult> MyProfile()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Detail), new { id = user.UserId });
    }

    [HttpGet("{id:long}/Detail")]
    public async Task<IActionResult> Detail(long id)
    {
        var user = await _userFacade.FetchUserAsync(id);
        UserDetailViewModel userDetail = user.Adapt<UserDetailViewModel>();

        return View(userDetail);
    }

    [HttpGet("Users")]
    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    [HttpGet("MyWishList")]
    public async Task<IActionResult> FetchMyWisList()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("User", "WishList", new { id = user.UserId });
    }

    [HttpGet("MyOrders")]
    public async Task<IActionResult> FetchMyOrders()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("User", "Order", new {id = user.UserId });
    }

    [HttpGet("MyReviews")]
    public async Task<IActionResult> FetchMyBookReviews()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("User" ,"BookReview", new { id = user.UserId });
    }

    [HttpGet("CreateOrder")]
    public async Task<IActionResult> CreateOrder()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("Create", "Order", new { userId = user.UserId });
    }
}