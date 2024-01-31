using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.Facades.BookReview;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.DTOs.BookReview.Update;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookReviewController : ControllerBase
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
        return Created(
            new Uri($"{Request.Path}/{bookReview.Id}", UriKind.Relative),
            bookReview
        );
        
    }

    [HttpPut]
    [Route("Update/{bookReviewId}")]
    public async Task<IActionResult> UpdateBookReview(long bookReviewId, UpdateBookReviewDto updateBookReviewDto)
    {
        return Ok(
            await _bookReviewFacade.UpdateBookReview(bookReviewId, updateBookReviewDto)
        );
    }

    [HttpGet]
    [Route("Book/{bookId}")]
    public async Task<IActionResult> FetchBookReviews(long bookId)
    {
        return Ok(
            await _bookReviewFacade.FindBookReviewsAsync(bookId)
        );
    }

    [HttpGet]
    [Route("User/{userId}")]
    public async Task<IActionResult> FetchUserReviews(long userId)
    {
        return Ok(
            await _bookReviewFacade.FindUserReviewsAsync(userId)
        );
    }

}
