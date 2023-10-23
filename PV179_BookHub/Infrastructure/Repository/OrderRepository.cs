using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
