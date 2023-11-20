using DataAccessLayer.Data;
using DataAccessLayer.Models.Preferences;

namespace Infrastructure.Repository.EntityRepositories;

public class WishListRepository : GenericRepository<WishList>
{
    public WishListRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
