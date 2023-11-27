using BusinessLayer.Facades.Book;
using BusinessLayer.Facades.Author;
using BusinessLayer.Facades.BookReview;
using BusinessLayer.Facades.User;
using BusinessLayer.Facades.Publisher;
using BusinessLayer.Mappers;
using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using BusinessLayer.Services.Book;
using BusinessLayer.Services.BookReview;
using BusinessLayer.Services.Order;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Facades.BookStore;
using BusinessLayer.Facades.Order;

namespace TestUtilities.MockedObjects;

public class MockedDependencyInjectionBuilder
{
    private IServiceCollection _serviceCollection = new ServiceCollection();

    public MockedDependencyInjectionBuilder AddMockedDBContext()
    {
        _serviceCollection = _serviceCollection
            .AddDbContext<BookHubDbContext>(options => options
                .UseInMemoryDatabase(MockedDbContext.RandomDBName));

        return this;

    }

    public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
        where T : class
    {
        _serviceCollection = _serviceCollection
            .AddScoped<T>(_ => objectToRegister);

        return this;
    }

    public MockedDependencyInjectionBuilder AddAutoMapper()
    {
        _serviceCollection = _serviceCollection
            .AddAutoMapper(typeof(AddressProfile))
            .AddAutoMapper(typeof(BookProfile))
            .AddAutoMapper(typeof(BookReviewProfile))
            .AddAutoMapper(typeof(BookStoreProfile))
            .AddAutoMapper(typeof(OrderProfile))
            .AddAutoMapper(typeof(UserProfile))
            .AddAutoMapper(typeof(WishListItemProfile))
            .AddAutoMapper(typeof(WishListProfile))
            .AddAutoMapper(typeof(AuthorBookAssociationProfile));

        return this;
    }

    public MockedDependencyInjectionBuilder AddRepositories()
    {
        _serviceCollection = _serviceCollection
            .AddScoped<IGenericRepository<Author, long>, GenericRepository<Author, long>>()
            .AddScoped<IGenericRepository<Publisher, long>, GenericRepository<Publisher, long>>()
            .AddScoped<IGenericRepository<BookStore, long>, GenericRepository<BookStore, long>>()
            .AddScoped<IGenericRepository<AuthorBookAssociation, long>, GenericRepository<AuthorBookAssociation, long>>()
            .AddScoped<IGenericRepository<InventoryItem, long>, GenericRepository<InventoryItem, long>>()
            .AddScoped<IGenericRepository<Book, long>, GenericRepository<Book, long>>()
            .AddScoped<IGenericRepository<WishList, long>, GenericRepository<WishList, long>>()
            .AddScoped<IGenericRepository<WishListItem, long>, GenericRepository<WishListItem, long>>()
            .AddScoped<IGenericRepository<Order, long>, GenericRepository<Order, long>>()
            .AddScoped<IGenericRepository<OrderItem, long>, GenericRepository<OrderItem, long>>()
            .AddScoped<IGenericRepository<BookReview, long>, GenericRepository<BookReview, long>>()
            .AddScoped<IGenericRepository<User, long>, GenericRepository<User, long>>()
            .AddScoped<IGenericRepository<Address, long>, GenericRepository<Address, long>>();

        return this;
    }

    public MockedDependencyInjectionBuilder AddUnitOfWork()
    {
        _serviceCollection = _serviceCollection
            .AddScoped<IUnitOfWork, BookHubUnitOfWork>();

        return this;
    }

    public MockedDependencyInjectionBuilder AddServices()
    {
        _serviceCollection = _serviceCollection
            .AddScoped<IBookService, BookService>()
            .AddScoped<IGenericService<User, long>, GenericService<User, long>>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IBookReviewService, BookReviewService>()
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IGenericService<Publisher, long>, GenericService<Publisher, long>>();

        return this;
    }

    public MockedDependencyInjectionBuilder AddFacades()
    {
        _serviceCollection = _serviceCollection
            .AddScoped<IBookFacade, BookFacade>()
            .AddScoped<IUserFacade, UserFacade>()
            .AddScoped<IBookReviewFacade, BookReviewFacade>()
            .AddScoped<IOrderFacade, OrderFacade>();
            .AddScoped<IBookStoreFacade, BookStoreFacade>();
            .AddScoped<IBookStoreFacade, BookStoreFacade>()
            .AddScoped<IInventoryItemFacade, InventoryItemFacade>();

        return this;
    }

    public ServiceProvider Create()
    {
        return _serviceCollection.BuildServiceProvider();
    }
}
