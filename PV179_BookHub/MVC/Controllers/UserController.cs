﻿using BusinessLayer.Facades.User;
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
    private readonly UserManager<User> _userManager;
    private readonly IUserFacade _userFacade;

    public UserController(UserManager<User> userManager, IUserFacade userFacade)
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

        return RedirectToAction(nameof(Detail), new { id = user.Id });
    }

    [HttpGet("{id:long}/Detail")]
    public async Task<IActionResult> Detail(long id)
    {
        var user = await _userFacade.FetchUserAsync(id);
        UserDetailViewModel userDetail = user.Adapt<UserDetailViewModel>();

        return View(userDetail);
    }

    [HttpGet("Users")]
    public async Task<IActionResult> Users(bool success)
    {
        var users = await _userManager.Users.ToListAsync();
        if (success)
        {
            ViewBag.Message = "Operation was successful.";
        }
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

        return RedirectToAction("User", "WishList", new { id = user.Id });
    }

    [HttpGet("MyOrders")]
    public async Task<IActionResult> FetchMyOrders()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("User", "Order", new {id = user.Id });
    }

    [HttpGet("MyReviews")]
    public async Task<IActionResult> FetchMyBookReviews()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("User" ,"BookReview", new { id = user.Id });
    }

    [HttpGet("CreateOrder")]
    public async Task<IActionResult> CreateOrder()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        return RedirectToAction("Create", "Order", new { userId = user.Id });
    }
}