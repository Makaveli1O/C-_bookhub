using BusinessLayer.Facades.Book;
using BusinessLayer.Services.Book;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;


namespace TestUtilities.MockedObjects
{
    public class MockedDependencyInjectionBuilder
    {
        private IServiceCollection _serviceCollection = new ServiceCollection();

        public MockedDependencyInjectionBuilder AddMockdDBContext()
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
                .AddScoped<IBookService, BookService>();

            return this;
        }

        public MockedDependencyInjectionBuilder AddFacades()
        {
            _serviceCollection = _serviceCollection
                .AddScoped<IBookFacade, BookFacade>();

            return this;
        }

        public ServiceProvider Create()
        {
            return _serviceCollection.BuildServiceProvider();
        }
    }

}
