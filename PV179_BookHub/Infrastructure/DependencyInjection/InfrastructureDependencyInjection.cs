using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Author, long>, GenericRepository<Author, long>>();
        services.AddScoped<IGenericRepository<Publisher, long>, GenericRepository<Publisher, long>>();
        services.AddScoped<IGenericRepository<BookStore, long>, GenericRepository<BookStore, long>>();
        services.AddScoped<IGenericRepository<AuthorBookAssociation, long>, GenericRepository<AuthorBookAssociation, long>>();
        services.AddScoped<IGenericRepository<InventoryItem, long>, GenericRepository<InventoryItem, long>>();
        services.AddScoped<IGenericRepository<Book, long>, GenericRepository<Book, long>>();
        services.AddScoped<IGenericRepository<WishList, long>, GenericRepository<WishList, long>>();
        services.AddScoped<IGenericRepository<WishListItem, long>, GenericRepository<WishListItem, long>>();
        services.AddScoped<IGenericRepository<Order, long>, GenericRepository<Order, long>>();
        services.AddScoped<IGenericRepository<OrderItem, long>, GenericRepository<OrderItem, long>>();
        services.AddScoped<IGenericRepository<User, long>, GenericRepository<User, long>>();
        services.AddScoped<IGenericRepository<BookReview, long>, GenericRepository<BookReview, long>>();
        services.AddScoped<IGenericRepository<Address, long>, GenericRepository<Address, long>>();

        services.AddScoped<IUnitOfWork, BookHubUnitOfWork>();
    }
}
