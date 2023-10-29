using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository.EntityRepositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
