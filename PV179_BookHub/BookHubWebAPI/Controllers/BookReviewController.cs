using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.Facades.BookReview;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.BookReview.Update;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookReviewController : Controller
{
    private readonly IBookReviewFacade _bookReviewFacade;
    public BookReviewController(IBookReviewFacade bookReviewFacade)
    {
        _bookReviewFacade = bookReviewFacade;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateBookReview(CreateBookReviewDto createBookReviewDto)
    {
        var bookReview = await _bookReviewFacade.CreateBookReview(createBookReviewDto);

        return Ok(bookReview);
    }

    [HttpPut]
    [Route("Update/{bookReviewId}")]
    public async Task<IActionResult> UpdateBookReview(long bookReviewId, UpdateBookReviewDto updateBookReviewDto)
    {
        var bookReview = await _bookReviewFacade.UpdateBookReview(bookReviewId, updateBookReviewDto);

        return Ok(bookReview);
    }

    [HttpGet]
    [Route("Book/{bookId}")]
    public async Task<IActionResult> FetchBookReviews(long bookId)
    {
        var bookReviews = await _bookReviewFacade.FindBookReviewsAsync(bookId);

        return Ok(bookReviews);
    }

    [HttpGet]
    [Route("User/{userId}")]
    public async Task<IActionResult> FetchUserReviews(long userId)
    {
        var bookReviews = await _bookReviewFacade.FindUserReviewsAsync(userId);

        return Ok(bookReviews);
    }

}
