using BusinessLayer.Services.Author;
using BusinessLayer.Services;
using BusinessLayer.Services.Book;
using TestUtilities.MockedObjects;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedData;
using BusinessLayer.DTOs.Book.Create;
using DataAccessLayer.Models.Publication;
using NSubstitute.ExceptionExtensions;
using BusinessLayer.Exceptions;
using BusinessLayer.Facades.Book;

namespace BusinessLayer.Tests.FacadeTests;

public class BookFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private readonly IBookService _bookServiceMock;
    private readonly IAuthorService _authorServiceMock;
    private readonly IGenericService<Publisher, long> _publisherServiceMock;

    public BookFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddUnitOfWork()
            .AddRepositories()
            .AddAutoMapper()
            .AddServices()
            .AddFacades()
            .AddMockedDBContext();

        _bookServiceMock = Substitute.For<IBookService>();
        _authorServiceMock = Substitute.For<IAuthorService>();
        _publisherServiceMock = Substitute.For<IGenericService<Publisher, long>>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_bookServiceMock)
            .AddScoped(_authorServiceMock)
            .AddScoped(_publisherServiceMock)
            .Create();
    }

    [Fact]
    public async Task CreateBook_ShouldReturnSuccess()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);
        
        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);
        
        var authors = TestDataInitializer.GetTestAuthors().Where(x => x.Id == authorAssociates.AuthorId);
        var publisher = TestDataInitializer.GetTestPublishers().First(x => x.Id == book.PublisherId);

        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = authors.Select(x => x.Id),
            BookGenre = book.BookGenre,
            Description = book.Description,
            Price = book.Price,
        };

        _bookServiceMock.CreateAsync(Arg.Any<Book>()).Returns(book);
        _authorServiceMock.FetchAllAuthorsByIdsAsync(Arg.Any<IEnumerable<long>>()).Returns(authors);
        _publisherServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(publisher);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookFacade = scope.ServiceProvider.GetRequiredService<IBookFacade>();

            var result = await bookFacade.CreateBookAsync(bookDto);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
            await _bookServiceMock.Received(1).CreateAsync((Arg.Any<Book>()));
            await _authorServiceMock.Received(1).FetchAllAuthorsByIdsAsync(Arg.Any<IEnumerable<long>>());
            await _publisherServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task CreateBookWithoutAuthor_ShouldThrowException()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);
       
        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = new[]{ (long)1 },
            BookGenre = book.BookGenre,
            Description = book.Description,
            Price = book.Price,
        };

        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);

        var publisher = TestDataInitializer.GetTestPublishers().First(x => x.Id == book.PublisherId);

        _bookServiceMock.CreateAsync(Arg.Any<Book>()).Returns(book);
        _authorServiceMock.FetchAllAuthorsByIdsAsync(Arg.Any<IEnumerable<long>>()).Throws(new NoSuchEntityException<IEnumerable<long>>(typeof(Author), new List<long> { 1 }));
        _publisherServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(publisher);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookFacade = scope.ServiceProvider.GetRequiredService<IBookFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<IEnumerable<long>>>(async () => await bookFacade.CreateBookAsync(bookDto));
        }
    }
    [Fact]
    public async Task CreateBookWithoutPublisher_ShouldThrowException()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);

        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);

        var author = TestDataInitializer.GetTestAuthors().First(x => x.Id == authorAssociates.AuthorId);

        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = new[] { author.Id },
            BookGenre = book.BookGenre,
            Description = book.Description,
            Price = book.Price,
        };

        _bookServiceMock.CreateAsync(Arg.Any<Book>()).Returns(book);
        _authorServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(author);
        _publisherServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(Publisher), book.PublisherId));

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookFacade = scope.ServiceProvider.GetRequiredService<IBookFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await bookFacade.CreateBookAsync(bookDto));
        }
    }

    [Fact]
    public async Task UpdateBook_ShouldReturnSuccess()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);
        
        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);
        var author = TestDataInitializer.GetTestAuthors().First(x => x.Id == authorAssociates.AuthorId);

        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = new[] { author.Id },
            BookGenre = book.BookGenre,
            Description = book.Description,
            Price = 1.5,
        };

        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);
        _bookServiceMock.UpdateAsync(Arg.Any<Book>()).Returns(book);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookFacade = scope.ServiceProvider.GetRequiredService<IBookFacade>();

            var result = await bookFacade.UpdateBookAsync(book.Id,bookDto);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
            await _bookServiceMock.Received(1).UpdateAsync((Arg.Any<Book>()));
            await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task UpdateNotExistingBook_ShouldThrowException()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);

        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);
        var author = TestDataInitializer.GetTestAuthors().First(x => x.Id == authorAssociates.AuthorId);

        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = new[] { author.Id },
            BookGenre = book.BookGenre,
            Description = book.Description,
            Price = 1.5,
        };

        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(Book), book.Id));
        _bookServiceMock.UpdateAsync(Arg.Any<Book>()).Returns(book);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookFacade = scope.ServiceProvider.GetRequiredService<IBookFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await bookFacade.UpdateBookAsync(book.Id, bookDto));
        }
    }

    [Fact]
    public async Task FindBook_ShouldReturnSuccess()
    {
        long id = 1;
        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == id);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);
        
        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var bookFacade = scope.ServiceProvider.GetRequiredService<IBookFacade>();
            var result = await bookFacade.FindBookByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
        }
    }
}
