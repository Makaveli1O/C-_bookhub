using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers;

[Route("User")]
public class UserController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;

    public UserController(UserManager<LocalIdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
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

        return RedirectToAction("SingleUserWishList", "WishList", new { id = user.UserId });
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