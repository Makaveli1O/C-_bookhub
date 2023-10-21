using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository;

public class WishListItemRepository : GenericRepository<WishListItem>
{
    public WishListItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
