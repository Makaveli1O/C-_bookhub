using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.User.View;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly SignInManager<LocalIdentityUser> _signInManager;

    public AccountController(UserManager<LocalIdentityUser> userManager, SignInManager<LocalIdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }

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
            
            var user = new LocalIdentityUser
            {
                UserName = model.Email.Split("@")[0],
                Email = model.Email,
                User = new User
                {
                    // For now we just set these dummy values. some fields will be
                    // removed from User once we fully integrate with identity framework.
                    UserName = model.Email.Split("@")[0]
                }
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(LoginSuccess), nameof(AccountController).Replace("Controller", ""));
            }
            ModelState.AddModelError(String.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }

    public IActionResult LoginSuccess()
    {
        return View();
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
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }
        return View(changePasswordViewModel);

    }

}

