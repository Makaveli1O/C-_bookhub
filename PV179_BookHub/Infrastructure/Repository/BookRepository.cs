using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository;

public class BookRepository : GenericRepository<Book>
{
    public BookRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
