using DataAccessLayer.Models;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Book> BookRepository { get; }
    void Commit();
    Task CommitAsync();
    void Rollback();
}
