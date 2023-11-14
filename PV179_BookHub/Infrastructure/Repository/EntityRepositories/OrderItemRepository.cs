using DataAccessLayer.Data;
using DataAccessLayer.Models.Purchasing;

namespace Infrastructure.Repository.EntityRepositories;

public class OrderItemRepository : GenericRepository<OrderItem>
{
    public OrderItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
