using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository;

public class WishListRepository : GenericRepository<WishList>
{
    public WishListRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
