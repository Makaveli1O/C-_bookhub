using DataAccessLayer.Data;
using DataAccessLayer.Models.Logistics;

namespace Infrastructure.Repository.EntityRepositories;
public class InventoryItemRepository : GenericRepository<InventoryItem>
{
    public InventoryItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
