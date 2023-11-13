using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository.EntityRepositories;

public class AddressRepository : GenericRepository<Address>
{
    public AddressRepository(BookHubDbContext dbContext) : base(dbContext)
    {

    }
}
