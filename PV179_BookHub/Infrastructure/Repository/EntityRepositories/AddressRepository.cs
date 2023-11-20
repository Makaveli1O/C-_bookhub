using DataAccessLayer.Data;
using DataAccessLayer.Models.Logistics;

namespace Infrastructure.Repository.EntityRepositories;

public class AddressRepository : GenericRepository<Address>
{
    public AddressRepository(BookHubDbContext dbContext) : base(dbContext)
    {

    }
}
