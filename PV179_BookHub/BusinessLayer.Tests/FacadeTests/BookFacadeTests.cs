using BusinessLayer.Services.Author;
using BusinessLayer.Services;
using TestUtilities.MockedObjects;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedData;
using BusinessLayer.DTOs.Book.Create;
using DataAccessLayer.Models.Publication;
using NSubstitute.ExceptionExtensions;
using BusinessLayer.Exceptions;
using BusinessLayer.Facades.Book;
using BusinessLayer.Services.AuthorBookAssociation;
using BusinessLayer.DTOs.Book.Update;

namespace BusinessLayer.Tests.FacadeTests;

public class BookFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private readonly IGenericService<Book, long> _bookServiceMock;
    private readonly IAuthorService _authorServiceMock;
    private readonly IGenericService<Publisher, long> _publisherServiceMock;
    private readonly IAuthorBookAsssociationService _authorBookAsssociationServiceMock;

    public BookFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _bookServiceMock = Substitute.For<IGenericService<Book, long>>();
        _authorServiceMock = Substitute.For<IAuthorService>();
        _publisherServiceMock = Substitute.For<IGenericService<Publisher, long>>();
        _authorBookAsssociationServiceMock = Substitute.For<IAuthorBookAsssociationService>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_bookServiceMock)
            .AddScoped(_authorServiceMock)
            .AddScoped(_publisherServiceMock)
            .AddScoped(_authorBookAsssociationServiceMock)
            .Create();
    }

    [Fact]
    public async Task CreateBook_ShouldReturnNewBook()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);
        
        var authorBookAssociations = TestDataInitializer.GetTestAuthorBookAssociations().Where(x => x.BookId == book.Id).ToList();
        
        var authors = TestDataInitializer.GetTestAuthors().Where(x => authorBookAssociations.Any(y => y.AuthorId == x.Id)).ToList();
        var publisher = TestDataInitializer.GetTestPublishers().First(x => x.Id == book.PublisherId);

        var authorBookAssocDtos = new List<AuthorAssocDto>();
        foreach (var author in authors)
        {
            var authorBookAssoc = authorBookAssociations.First(x => x.AuthorId == author.Id);
            authorBookAssocDtos.Add(new AuthorAssocDto() { Id = author.Id, IsPrimary = authorBookAssoc.IsPrimary });
        }

        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = authorBookAssocDtos,
            BookGenre = book.BookGenre,
            Description = book.Description,
            Price = book.Price
        };

        book.Authors = authors;
        book.Publisher = publisher;
        book.AuthorBookAssociations = authorBookAssociations;

        _bookServiceMock.CreateAsync(Arg.Any<Book>()).Returns(book);
        _authorServiceMock.FetchAllAuthorsByIdsAsync(Arg.Any<IEnumerable<long>>()).Returns(authors);
        _publisherServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(publisher);
        _authorBookAsssociationServiceMock.CreateMultipleAssociationsAsync(Arg.Any<Book>(), 
            Arg.Any<IEnumerable<Tuple<long, bool>>>(), Arg.Any<bool>()).Returns(authorBookAssociations);

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
            await _authorBookAsssociationServiceMock.Received(1).CreateMultipleAssociationsAsync(
                Arg.Any<Book>(), Arg.Any<IEnumerable<Tuple<long, bool>>>(), Arg.Any<bool>());
        }
    }

    [Fact]
    public async Task CreateBookWithoutAuthor_ShouldThrowExceptionAuthorForTheBookDoesNotExist()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);
       
        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = new List<AuthorAssocDto>(),
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
    public async Task CreateBookWithoutPublisher_ShouldThrowExceptionPublisherForTheBookDoesNotExist()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);

        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);

        var author = TestDataInitializer.GetTestAuthors().First(x => x.Id == authorAssociates.AuthorId);

        var bookDto = new CreateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
            AuthorIds = new List<AuthorAssocDto>() { new AuthorAssocDto() { Id = 1, IsPrimary = true} },
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
    public async Task UpdateBook_ShouldReturnUpdatedBook()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);
        
        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);
        var author = TestDataInitializer.GetTestAuthors().First(x => x.Id == authorAssociates.AuthorId);

        var bookDto = new UpdateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
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

            var result = await bookFacade.UpdateBookAsync(book.Id, bookDto);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Price, result.Price);
            await _bookServiceMock.Received(1).UpdateAsync((Arg.Any<Book>()));
            await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task UpdateNotExistingBook_ShouldThrowExceptionBookDoesNotExist()
    {
        var book = TestDataInitializer.GetTestBooks().ElementAt(0);

        var authorAssociates = TestDataInitializer.GetTestAuthorBookAssociations().First(x => x.BookId == book.Id);
        var author = TestDataInitializer.GetTestAuthors().First(x => x.Id == authorAssociates.AuthorId);

        var bookDto = new UpdateBookDto()
        {
            Title = book.Title,
            ISBN = book.ISBN,
            PublisherId = book.PublisherId,
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
    public async Task FindBook_ShouldReturnExistingBook()
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
