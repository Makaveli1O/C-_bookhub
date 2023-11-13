using AutoMapper;
using BookHubWebAPI.Api.BookReview.Create;
using BookHubWebAPI.Api.BookReview.View;
using DataAccessLayer.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BookHubWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookReviewController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public BookReviewController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateBookReview(CreateBookReviewDto createBookReviewDto)
    {
        var bookReview = _mapper.Map<BookReview>(createBookReviewDto);

        await _unitOfWork.BookReviewRepository.AddAsync(bookReview);
        await _unitOfWork.CommitAsync();

        return Created(
             new Uri($"{Request.Path}/{bookReview.Id}", UriKind.Relative),
            _mapper.Map<GeneralBookReviewViewDto>(bookReview));
    }

    [HttpPut]
    [Route("Update/{bookReviewId}")]
    public async Task<IActionResult> UpdateBookReview(long bookReviewId, string? bookReviewDescription)
    {
        var bookReview = await _unitOfWork.BookReviewRepository.GetByIdAsync(bookReviewId);

        if (bookReview != null)
        {
            bookReview.Description = bookReviewDescription;
            _unitOfWork.BookReviewRepository.Update(bookReview);
            await _unitOfWork.CommitAsync();
        }

        return Ok(
            _mapper.Map<GeneralBookReviewViewDto>(bookReview)
            );
    }

    [HttpGet]
    [Route("Book/{bookId}")]
    public async Task<IActionResult> FetchBookReviews(long bookId)
    {
        var bookReviews = await _unitOfWork.BookReviewRepository.GetAllFilteredAsync(
            review => review.BookId == bookId
            );
       

        return Ok(
            _mapper.Map<IEnumerable<GeneralBookReviewViewDto>>(bookReviews)
            );
    }

    [HttpGet]
    [Route("User/{userId}")]
    public async Task<IActionResult> FetchUserReviews(long userId)
    {
        var bookReviews = 
            await _unitOfWork.BookReviewRepository.GetAllFilteredAsync(
                user => user.Reviewer.Id == userId
                );

        return Ok(
            _mapper.Map<IEnumerable<GeneralBookReviewViewDto>>(bookReviews));
    }

}
