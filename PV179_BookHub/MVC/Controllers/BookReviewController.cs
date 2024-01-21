using BusinessLayer.Facades.BookReview;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MVC.Controllers;

[Route("BookReview")]
public class BookReviewController : Controller
{
    private readonly IBookReviewFacade _bookReviewFacade;
    private readonly UserManager<User> _userManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public BookReviewController(IBookReviewFacade bookReviewFacade, UserManager<User> userManager)
    {
        _bookReviewFacade = bookReviewFacade;
        _userManager = userManager;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    }

    [HttpGet("User/{id:long}")]
    //[AllowAnonymous]
    public async Task<JsonResult> SingleUserBookReviews(long id)
    {
        return Json(await _bookReviewFacade.FindUserReviewsAsync(id));
    }
}
