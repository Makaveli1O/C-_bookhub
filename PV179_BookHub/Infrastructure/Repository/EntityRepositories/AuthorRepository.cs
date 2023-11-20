
using DataAccessLayer.Data;
using DataAccessLayer.Models.Publication;

namespace Infrastructure.Repository.EntityRepositories;

public class AuthorRepository : GenericRepository<Author>
{
    public AuthorRepository(BookHubDbContext bookHubDbContext) : base(bookHubDbContext)
    {

    }
}
