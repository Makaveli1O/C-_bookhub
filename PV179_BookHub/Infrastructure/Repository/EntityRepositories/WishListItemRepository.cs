using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository.EntityRepositories;

public class WishListItemRepository : GenericRepository<WishListItem>
{
    public WishListItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
