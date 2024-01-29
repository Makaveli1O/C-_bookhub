using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Email is already in use.");
                return View(model);
            }
            
            var user = new User
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                Role = UserRole.User
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            var roleResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

            if (result.Succeeded && roleResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            foreach (var error in roleResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
        }

        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }

    public async Task<IActionResult> ResetPassword(string id, bool success)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) 
        {
            return RedirectToAction("Index", "User");
        }
        ViewBag.UserName = user.UserName;
        if (success) { 
            ViewBag.Message = "Password Successfully Changed!";
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(string id, ResetPasswordViewModel changePasswordViewModel)
    {
        var res = false;
        var user = await _userManager.FindByIdAsync(id);
        if (user != null && ModelState.IsValid)
        {
            ViewBag.UserName = user.UserName;
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePasswordViewModel.Password);
            res = result.Succeeded;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPassword), new { id, success = res });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(changePasswordViewModel);
    }

    public IActionResult RegisterManager()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterManager(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Email is already in use.");
                return View(model);
            }
            var user = new User
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                Role = UserRole.Manager
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            var roleResult = await _userManager.AddToRoleAsync(user, UserRole.Manager.ToString());

            if (result.Succeeded && roleResult.Succeeded)
            {
                return RedirectToAction("Users", "User", new { success = true });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            foreach (var error in roleResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> DeleteAccount(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return RedirectToAction("Users", "User");
        }
        ViewBag.UserName = user.UserName;
        return View();
    }

    [HttpPost, ActionName("DeleteAccount")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAccountPost(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users", "User", new { success = true });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View();
    }

}

