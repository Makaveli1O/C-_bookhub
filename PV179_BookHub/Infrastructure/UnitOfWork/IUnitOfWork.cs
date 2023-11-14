using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Author> AuthorRepository { get; }
    IGenericRepository<Publisher> PublisherRepository { get; }
    IGenericRepository<Book> BookRepository { get; }
    IGenericRepository<AuthorBookAssociation> AuthorBookAssociationRepository { get; }
    IGenericRepository<BookStore> BookStoreRepository { get; }
    IGenericRepository<InventoryItem> InventoryItemRepository { get; }
    IGenericRepository<WishList> WishListRepository { get; }
    IGenericRepository<WishListItem> WishListItemRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<OrderItem> OrderItemRepository { get; }
    IGenericRepository<User> UserRepository { get; }  
    IGenericRepository<BookReview> BookReviewRepository { get; }
    IGenericRepository<Address> AddressRepository { get; }
    void Commit();
    Task CommitAsync();
    void Rollback();
}
