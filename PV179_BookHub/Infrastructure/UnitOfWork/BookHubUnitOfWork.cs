using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

public class BookHubUnitOfWork : IUnitOfWork
{
    private readonly BookHubDbContext _dbContext;
    private BookRepository? _bookRepository;
    private WishListRepository? _wishListRepository;
    private WishListItemRepository? _wishListItemRepository;
    private OrderRepository? _orderRepository;
    private OrderItemRepository? _orderItemRepository;

    public IGenericRepository<Book> BookRepository => _bookRepository ??= new BookRepository(_dbContext);
    public IGenericRepository<WishList> WishListRepository => _wishListRepository ??= new WishListRepository(_dbContext);
    public IGenericRepository<WishListItem> WishListItemRepository => _wishListItemRepository ??= new WishListItemRepository(_dbContext);
    public IGenericRepository<Order> OrderRepository => _orderRepository ??= new OrderRepository(_dbContext);
    public IGenericRepository<OrderItem> OrderItemRepository => _orderItemRepository ??= new OrderItemRepository(_dbContext);

    public BookHubUnitOfWork(BookHubDbContext dbContext)
    {
        _dbContext = dbContext;
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
