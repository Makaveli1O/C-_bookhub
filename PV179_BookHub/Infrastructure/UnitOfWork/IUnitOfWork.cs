using DataAccessLayer.Models;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Book> BookRepository { get; }
    IGenericRepository<WishList> WishListRepository { get; }
    IGenericRepository<WishListItem> WishListItemRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<OrderItem> OrderItemRepository { get; }
    IGenericRepository<User> UserRepository { get; }    
    void Commit();
    Task CommitAsync();
    void Rollback();
}
