using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers;

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
}
