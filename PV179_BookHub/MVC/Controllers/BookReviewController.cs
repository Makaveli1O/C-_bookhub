using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.BookReview;
using BusinessLayer.Facades.User;
using DataAccessLayer.Models.Account;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.BookReview;

namespace MVC.Controllers;

[Route("BookReview")]
public class BookReviewController : Controller
{
    private readonly IBookReviewFacade _bookReviewFacade;
    private readonly IBookFacade _bookFacade;
    private readonly IUserFacade _userFacade;
    private readonly UserManager<User> _userManager;

    public BookReviewController(
        IBookReviewFacade bookReviewFacade,
        IBookFacade bookFacade,
        IUserFacade userFacade,
        UserManager<User> userManager
        )
    {
        _bookReviewFacade = bookReviewFacade;
        _bookFacade = bookFacade;
        _userFacade = userFacade;
        _userManager = userManager;
    }

    [HttpGet("User/{userId:long}")]
    [AllowAnonymous]
    public async Task<IActionResult> SingleUserBookReviews(long userId)
    {
        var userReviews = await _bookReviewFacade.FindUserReviewsAsync(userId);

        List<BookReviewViewModel> model = new List<BookReviewViewModel>();
        foreach (var userReview in userReviews)
        {
            var modelElement = userReview.Adapt<BookReviewViewModel>();
            modelElement.Title = (await _bookFacade.FindBookByIdAsync(userReview.BookId)).Title;
            modelElement.ReviewerName = (await _userFacade.FetchUserAsync(userReview.ReviewerId)).UserName;
            model.Add(modelElement);
        }

        return View("UserReviews", model);
    }

    [HttpGet("Book/{id:long}")]
    public async Task<IActionResult> BookReviews(long id)
    {
        var bookReviews = await _bookReviewFacade.FindBookReviewsAsync(id);

        List<BookReviewViewModel> model = new List<BookReviewViewModel>();
        foreach (var bookReview in bookReviews)
        {
            var modelElement = bookReview.Adapt<BookReviewViewModel>();
            modelElement.Title = (await _bookFacade.FindBookByIdAsync(bookReview.BookId)).Title;
            modelElement.ReviewerName = (await _userFacade.FetchUserAsync(bookReview.ReviewerId)).UserName;
            model.Add(modelElement);
        }

        

        return View(model);
    }
}
