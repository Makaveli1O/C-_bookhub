using DataAccessLayer.Data;
using DataAccessLayer.Models.Publication;
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
            .Include(book => book.Associations)
                .ThenInclude(assoc => assoc.Author)
            .Include(book => book.Publisher)
            .FirstOrDefaultAsync(book => book.Id == id);
    }

    public override async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbContext.Books
            .Include(book => book.Associations)
                .ThenInclude(assoc => assoc.Author)
            .Include(book => book.Publisher)
            .ToListAsync();
    }
}
