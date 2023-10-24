using DataAccessLayer.Data;
using DataAccessLayer.Models;


namespace Infrastructure.Repository;

public class BookReviewRepository : GenericRepository<BookReview>
{
    public BookReviewRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
