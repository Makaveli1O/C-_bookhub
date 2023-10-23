using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(BookHubDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
