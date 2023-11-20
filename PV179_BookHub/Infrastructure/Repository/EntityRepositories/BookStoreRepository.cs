using DataAccessLayer.Data;
using DataAccessLayer.Models.Logistics;

namespace Infrastructure.Repository.EntityRepositories;

public class BookStoreRepository : GenericRepository<BookStore>
{
    public BookStoreRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
