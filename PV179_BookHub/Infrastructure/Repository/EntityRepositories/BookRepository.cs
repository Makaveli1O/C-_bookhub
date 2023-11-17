using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.EntityRepositories;

public class BookRepository : GenericRepository<Book>
{
    public BookRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Book?> GetByIdAsync(long id)
    {
        return await _dbContext.Books
            .Include(book => book.Reviews)
            .FirstAsync(book => book.Id == id);
    }
}
