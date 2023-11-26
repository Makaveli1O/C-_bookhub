using BusinessLayer.DTOs.BookReview.Create;
using BusinessLayer.DTOs.BookReview.View;
using BusinessLayer.DTOs.BookReview.Update;

namespace BusinessLayer.Facades.BookReview;

public interface IBookReviewFacade
{
    Task<List<GeneralBookReviewViewDto>> FindBookReviewsAsync(long id);
    Task<List<GeneralBookReviewViewDto>> FindUserReviewsAsync(long id);
    Task<DetailedBookReviewViewDto> CreateBookReview(CreateBookReviewDto createBookReviewDto);
    Task<GeneralBookReviewViewDto> UpdateBookReview(long id, UpdateBookReviewDto updateBookReviewDto);
}
