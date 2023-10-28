using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository.EntityRepositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(BookHubDbContext dbContext) : base(dbContext)
    {

    }
}
