using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository.EntityRepositories;

public class BookStoreRepository : GenericRepository<BookStore>
{
    public BookStoreRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
