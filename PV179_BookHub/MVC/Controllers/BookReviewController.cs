using BusinessLayer.Facades.BookReview;
using DataAccessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

[Route("BookReview")]
public class BookReviewController : Controller
{
    private readonly IBookReviewFacade _bookReviewFacade;

    public BookReviewController(IBookReviewFacade bookReviewFacade, UserManager<User> userManager)
    {
        _bookReviewFacade = bookReviewFacade;
    }

    [HttpGet("User/{id:long}")]
    //[AllowAnonymous]
    public async Task<JsonResult> SingleUserBookReviews(long id)
    {
        return Json(await _bookReviewFacade.FindUserReviewsAsync(id));
    }
}
