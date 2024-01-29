using AutoMapper;
using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.DTOs.BookReview.Update;
using BusinessLayer.DTOs.BookReview.View;
using BusinessLayer.Services;
using BusinessLayer.Services.BookReview;


namespace BusinessLayer.Facades.BookReview;

public class BookReviewFacade : BaseFacade, IBookReviewFacade
{
    private readonly IBookReviewService _bookReviewService;
    private readonly IGenericService<BookEntity, long> _bookService;
    private readonly IGenericService<UserEntity, long> _userService;

    public BookReviewFacade(
        IMapper mapper,
        IGenericService<BookEntity, long> bookService,
        IGenericService<UserEntity, long> userService,
        IBookReviewService bookReviewService)
        : base(mapper, null, null)
    {
        _bookReviewService = bookReviewService;
        _bookService = bookService;
        _userService = userService;
    }

    public async Task<List<GeneralBookReviewViewDto>> FindBookReviewsAsync(long id)
    {
        var bookReviews = await _bookReviewService.FindByBookIdAsync(id);
        return _mapper.Map<List<GeneralBookReviewViewDto>>(bookReviews);
    }

    public async Task<List<GeneralBookReviewViewDto>> FindUserReviewsAsync(long userId)
    {
        var bookReviews = await _bookReviewService.FindByUserIdAsync(userId);
        return _mapper.Map<List<GeneralBookReviewViewDto>>(bookReviews);
    }

    public async Task<DetailedBookReviewViewDto> CreateBookReview(CreateBookReviewDto createBookReviewDto)
    {
        var book = await _bookService.FindByIdAsync(createBookReviewDto.BookId);
        var user = await _userService.FindByIdAsync(createBookReviewDto.ReviewerId);

        var bookReview = _mapper.Map<BookReviewEntity>(createBookReviewDto);
        bookReview.Reviewer = user;
        bookReview.Book = book;
        bookReview = await _bookReviewService.CreateAsync(bookReview);

        return _mapper.Map<DetailedBookReviewViewDto>(bookReview);
    }

    public async Task<GeneralBookReviewViewDto> UpdateBookReview(long id, UpdateBookReviewDto updateBookReviewDto)
    {
        var bookReview = await _bookReviewService.FindByIdAsync(id);

        bookReview.Description = updateBookReviewDto.Description ?? bookReview.Description;
        bookReview.Rating = updateBookReviewDto.Rating;

        bookReview = await _bookReviewService.UpdateAsync(bookReview);

        return _mapper.Map<GeneralBookReviewViewDto>(bookReview);
    }
}
