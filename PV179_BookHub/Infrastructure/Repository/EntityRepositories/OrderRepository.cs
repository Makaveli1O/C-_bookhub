using DataAccessLayer.Data;
using DataAccessLayer.Models.Purchasing;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.EntityRepositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Order?> GetByIdAsync(long id)
    {
        return await _dbContext.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);
    }
}
