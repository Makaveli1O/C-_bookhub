using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;
public class InventoryItemRepository : GenericRepository<InventoryItem>
{
    public InventoryItemRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
