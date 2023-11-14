using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;

namespace Infrastructure.Repository.EntityRepositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(BookHubDbContext dbContext) : base(dbContext)
    {

    }
}
