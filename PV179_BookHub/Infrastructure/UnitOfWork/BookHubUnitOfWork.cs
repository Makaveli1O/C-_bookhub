﻿using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Infrastructure.Repository;
using Infrastructure.Repository.EntityRepositories;

namespace Infrastructure.UnitOfWork;

public class BookHubUnitOfWork : IUnitOfWork
{
    private readonly BookHubDbContext _dbContext;
    private BookStoreRepository? _bookStoreRepository;
    private InventoryItemRepository? _inventoryItemRepository;
    private BookRepository? _bookRepository;
    private WishListRepository? _wishListRepository;
    private WishListItemRepository? _wishListItemRepository;
    private OrderRepository? _orderRepository;
    private OrderItemRepository? _orderItemRepository;
    private UserRepository? _userRepository;
    private BookReviewRepository? _bookReviewRepository;
    private AddressRepository? _addressRepository;

    public IGenericRepository<BookStore> BookStoreRepository => _bookStoreRepository ??= new BookStoreRepository(_dbContext);
    public IGenericRepository<InventoryItem> InventoryItemRepository => _inventoryItemRepository ??= new InventoryItemRepository(_dbContext);
    public IGenericRepository<Book> BookRepository => _bookRepository ??= new BookRepository(_dbContext);
    public IGenericRepository<WishList> WishListRepository => _wishListRepository ??= new WishListRepository(_dbContext);
    public IGenericRepository<WishListItem> WishListItemRepository => _wishListItemRepository ??= new WishListItemRepository(_dbContext);
    public IGenericRepository<Order> OrderRepository => _orderRepository ??= new OrderRepository(_dbContext);
    public IGenericRepository<OrderItem> OrderItemRepository => _orderItemRepository ??= new OrderItemRepository(_dbContext);
    public IGenericRepository<BookReview> BookReviewRepository => _bookReviewRepository ??= new BookReviewRepository(_dbContext);
    public IGenericRepository<User> UserRepository => _userRepository ??= new UserRepository(_dbContext);
    public IGenericRepository<Address> AddressRepository => _addressRepository ??= new AddressRepository(_dbContext);
    
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