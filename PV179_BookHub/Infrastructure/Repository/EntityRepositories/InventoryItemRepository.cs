using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository.EntityRepositories;
public class InventoryItemRepository : GenericRepository<InventoryItem>
{
    public InventoryItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
