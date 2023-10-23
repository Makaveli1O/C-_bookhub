using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository;

public class OrderItemRepository : GenericRepository<OrderItem>
{
    public OrderItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
