using BusinessLayer.Services.Book;
using DataAccessLayer.Models.Publication;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq.Expressions;
using TestUtilities.MockedData;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.ServiceTests;

public class BookServiceTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IGenericRepository<Book, long> _repositoryMock;
    private IUnitOfWork _uowMock;

    public BookServiceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _repositoryMock = Substitute.For<IGenericRepository<Book, long>>();
        _uowMock = Substitute.For<IUnitOfWork>();
        _uowMock.GetRepositoryByEntity<Book, long>().Returns(_repositoryMock);
    }
    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_repositoryMock)
            .AddScoped(_uowMock)
            .Create();
    }

    [Fact]
    public async Task CreateBook_ShouldReturnNewBook()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            var result = await bookService.CreateAsync(book);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
        }
    }


    [Fact]
    public async Task UpdateBook_ShouldReturnUpdatedBook()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);

        book.Price = 1.66;

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

            var result = await bookService.UpdateAsync(book);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
            _repositoryMock.Received(1).Update(Arg.Any<Book>());
        }
    }

    [Fact]
    public async Task FindBook_ShouldReturnExistingBook()
    {
        long id = 1;

        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == id);

        _repositoryMock.GetByIdAsync(Arg.Any<long>(), Arg.Any<Expression<Func<Book, object>>[]>()).Returns(book);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();
            var result = await bookService.FindByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
        }
    }
}

