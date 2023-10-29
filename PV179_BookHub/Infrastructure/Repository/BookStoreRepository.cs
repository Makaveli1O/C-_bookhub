using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class BookStoreRepository : GenericRepository<BookStore>
{
    public BookStoreRepository(BookHubDbContext dbContext) : base(dbContext)
    {
    }
}
