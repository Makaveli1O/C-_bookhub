using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.BookReview;

public class BookReviewService : GenericService<BookReviewEntity, long>, IBookReviewService
{
    public BookReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    public async Task<IEnumerable<BookReviewEntity>> FindByBookIdAsync(long id)
    {
        var bookReviews = await Repository.GetAllAsync(
                x => x.BookId == id
            );

        if ( bookReviews == null )
        {
            throw new NoSuchEntityException<long>(
                typeof(BookReviewEntity),
                id
            );
        }

        return bookReviews;
    }

    public async Task<IEnumerable<BookReviewEntity>> FindByUserIdAsync(long userId)
    {
        var bookReviews = await Repository.GetAllAsync(
                x=> x.ReviewerId == userId
            );

        if( bookReviews == null )
        {
            throw new NoSuchEntityException<long>(
                typeof(BookReviewEntity),
                userId
            );
        }

        return bookReviews;
    }
}
