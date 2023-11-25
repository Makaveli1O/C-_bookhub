using DataAccessLayer.Data;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using TestUtilities.MockedData;

namespace TestUtilities.MockedObjects;

public static class MockedDbContext
{
    public static string RandomDBName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDBContextOptions()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
                .UseInMemoryDatabase(RandomDBName)
                .Options;

        return dbContextOptions;
    }

    public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
    {
        var dbContextToMock = new BookHubDbContext(options);

        var dbContext = new MockedDbContextBuilder<BookHubDbContext>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options)
            .MockedDbContext;

        PrepareData(dbContext);

        return dbContext;
    }

    public static void PrepareData(BookHubDbContext bookHubDbContext)
    {
        bookHubDbContext.Authors.AddRange(TestDataInitializer.GetTestAuthors());
        bookHubDbContext.Publishers.AddRange(TestDataInitializer.GetTestPublishers());
        bookHubDbContext.Books.AddRange(TestDataInitializer.GetTestBooks());
        bookHubDbContext.AuthorBookAssociations.AddRange(TestDataInitializer.GetTestAuthorBookAssociations());

        bookHubDbContext.Users.AddRange(TestDataInitializer.GetTestUsers());
        bookHubDbContext.BookReviews.AddRange(TestDataInitializer.GetTestBookReviews());

        bookHubDbContext.Addresses.AddRange(TestDataInitializer.GetTestAddresses());
        bookHubDbContext.BookStores.AddRange(TestDataInitializer.GetTestBookStores());
        bookHubDbContext.InventoryItems.AddRange(TestDataInitializer.GetTestInventoryItems());

        bookHubDbContext.WishList.AddRange(TestDataInitializer.GetTestWishLists());
        bookHubDbContext.WishListItem.AddRange(TestDataInitializer.GetTestWishListItem());

        bookHubDbContext.Orders.AddRange(TestDataInitializer.GetTestOrderList());
        bookHubDbContext.OrderItems.AddRange(TestDataInitializer.GetTestOrderItems());
    }
}
