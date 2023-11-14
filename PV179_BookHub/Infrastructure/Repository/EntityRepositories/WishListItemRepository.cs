using DataAccessLayer.Data;
using DataAccessLayer.Models.Preferences;

namespace Infrastructure.Repository.EntityRepositories;

public class WishListItemRepository : GenericRepository<WishListItem>
{
    public WishListItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
