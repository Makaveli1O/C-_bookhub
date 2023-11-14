using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;

namespace Infrastructure.Repository.EntityRepositories;

public class BookReviewRepository : GenericRepository<BookReview>
{
    public BookReviewRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
