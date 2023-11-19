using DataAccessLayer.Data;
using DataAccessLayer.Models.Publication;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.EntityRepositories;

public class BookRepository : GenericRepository<Book>
{
    public BookRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
