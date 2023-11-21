using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Author, long> AuthorRepository { get; }
    IGenericRepository<Publisher, long> PublisherRepository { get; }
    IGenericRepository<Book, long> BookRepository { get; }
    IGenericRepository<AuthorBookAssociation, long> AuthorBookAssociationRepository { get; }
    IGenericRepository<BookStore, long> BookStoreRepository { get; }
    IGenericRepository<InventoryItem, long> InventoryItemRepository { get; }
    IGenericRepository<WishList, long> WishListRepository { get; }
    IGenericRepository<WishListItem, long> WishListItemRepository { get; }
    IGenericRepository<Order, long> OrderRepository { get; }
    IGenericRepository<OrderItem, long> OrderItemRepository { get; }
    IGenericRepository<User, long> UserRepository { get; }  
    IGenericRepository<BookReview, long> BookReviewRepository { get; }
    IGenericRepository<Address, long> AddressRepository { get; }

    IGenericRepository<TEntity, TKey> GetRepositoryByEntity<TEntity, TKey>() where TEntity : class;
    void Commit();
    Task CommitAsync();
    void Rollback();
}
