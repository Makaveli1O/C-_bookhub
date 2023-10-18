using DataAccessLayer;

namespace Infrastructure.UnitOfWork;

public class BookHubUnitOfWork : IUnitOfWork
{
    private readonly BookHubDbContext _dbContext;

    public BookHubUnitOfWork(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Commit()
    {
        _dbContext.SaveChanges();
    }

    public void Rollback()
    {
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}
