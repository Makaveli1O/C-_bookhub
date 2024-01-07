using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.Exceptions;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public class BookHubUnitOfWork : IUnitOfWork
{
    private readonly BookHubDbContext _dbContext;

    private GenericRepository<Author, long>? _authorRepository;
    private GenericRepository<Publisher, long>? _publisherRepository;
    private GenericRepository<BookStore, long>? _bookStoreRepository;
    private GenericRepository<AuthorBookAssociation, long>? _authorBookAssociationRepository;
    private GenericRepository<InventoryItem, long>? _inventoryItemRepository;
    private GenericRepository<Book, long>? _bookRepository;
    private GenericRepository<WishList, long>? _wishListRepository;
    private GenericRepository<WishListItem, long>? _wishListItemRepository;
    private GenericRepository<Order, long>? _orderRepository;
    private GenericRepository<OrderItem, long>? _orderItemRepository;
    private GenericRepository<User, long>? _userRepository;
    private GenericRepository<BookReview, long>? _bookReviewRepository;
    private GenericRepository<Address, long>? _addressRepository;

    public IGenericRepository<Author, long> AuthorRepository => _authorRepository ??= new GenericRepository<Author, long>(_dbContext);
    public IGenericRepository<Publisher, long> PublisherRepository => _publisherRepository ??= new GenericRepository<Publisher, long>(_dbContext);
    public IGenericRepository<BookStore, long> BookStoreRepository => _bookStoreRepository ??= new GenericRepository<BookStore, long>(_dbContext);
    public IGenericRepository<AuthorBookAssociation, long> AuthorBookAssociationRepository => _authorBookAssociationRepository ??= new GenericRepository<AuthorBookAssociation, long>(_dbContext);
    public IGenericRepository<InventoryItem, long> InventoryItemRepository => _inventoryItemRepository ??= new GenericRepository<InventoryItem, long>(_dbContext);
    public IGenericRepository<Book, long> BookRepository => _bookRepository ??= new GenericRepository<Book, long>(_dbContext);
    public IGenericRepository<WishList, long> WishListRepository => _wishListRepository ??= new GenericRepository<WishList, long>(_dbContext);
    public IGenericRepository<WishListItem, long> WishListItemRepository => _wishListItemRepository ??= new GenericRepository<WishListItem, long>(_dbContext);
    public IGenericRepository<Order, long> OrderRepository => _orderRepository ??= new GenericRepository<Order, long>(_dbContext);
    public IGenericRepository<OrderItem, long> OrderItemRepository => _orderItemRepository ??= new GenericRepository<OrderItem, long>(_dbContext);
    public IGenericRepository<BookReview, long> BookReviewRepository => _bookReviewRepository ??= new GenericRepository<BookReview, long>(_dbContext);
    public IGenericRepository<User, long> UserRepository => _userRepository ??= new GenericRepository<User, long>(_dbContext);
    public IGenericRepository<Address, long> AddressRepository => _addressRepository ??= new GenericRepository<Address, long>(_dbContext);

    public BookHubUnitOfWork(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IGenericRepository<TEntity, TKey> GetRepositoryByEntity<TEntity, TKey>() where TEntity : class
    {
        var repository = GetType()
            .GetProperties()
            .Where(x => x.PropertyType == typeof(IGenericRepository<TEntity, TKey>))
            .FirstOrDefault()?
            .GetValue(this);

        if (repository == null)
        {
            throw new RepositoryNotFoundException(typeof(TEntity));
        }
        else
        {
            return (IGenericRepository<TEntity, TKey>) repository;
        }
    }

    public void Commit()
    {
        _dbContext.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Rollback()
    {
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}
