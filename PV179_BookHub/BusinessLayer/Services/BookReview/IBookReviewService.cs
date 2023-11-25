using BookReviewEntity = DataAccessLayer.Models.Account.BookReview;
namespace BusinessLayer.Services.BookReview;

public interface IBookReviewService : IGenericService<BookReviewEntity, long>
{
    Task<IEnumerable<BookReviewEntity>> FindByBookIdAsync(long id);
    Task<IEnumerable<BookReviewEntity>> FindByUserIdAsync(long userId);
}
