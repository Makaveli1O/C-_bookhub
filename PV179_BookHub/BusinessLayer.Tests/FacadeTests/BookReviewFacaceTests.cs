using BusinessLayer.Services.Book;
using BusinessLayer.Services.BookReview;
using BusinessLayer.Services;
using TestUtilities.MockedObjects;
using DataAccessLayer.Models.Account;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.DTOs.BookReview.Create;
using TestUtilities.MockedData;
using BusinessLayer.Facades.BookReview;
using DataAccessLayer.Models.Publication;
using BusinessLayer.Exceptions;
using NSubstitute.ExceptionExtensions;
using BusinessLayer.DTOs.BookReview.Update;

namespace BusinessLayer.Tests.FacadeTests;

public class BookReviewFacaceTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IBookReviewService _bookReviewServiceMock;
    private IGenericService<Book, long> _bookServiceMock;
    private IGenericService<User, long> _userServiceMock;

    public BookReviewFacaceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _bookReviewServiceMock = Substitute.For<IBookReviewService>();
        _bookServiceMock = Substitute.For<IGenericService<Book, long>>();
        _userServiceMock = Substitute.For<IGenericService<User, long>>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_bookReviewServiceMock)
            .AddScoped(_bookServiceMock)
            .AddScoped(_userServiceMock)
            .Create();
    }

    [Fact]
    public async Task CreateBookReview_ShouldReturnNewBookReview()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);
        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == bookReview.BookId);
        var user = TestDataInitializer.GetTestUsers().First(x => x.Id == bookReview.ReviewerId);

        var createBookReview = new CreateBookReviewDto()
        {
            BookId = bookReview.BookId,
            ReviewerId = bookReview.ReviewerId,
            Description = bookReview.Description,
            Rating = bookReview.Rating
        };
        

        _bookReviewServiceMock.CreateAsync(Arg.Any<BookReview>()).Returns(bookReview);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();

            var result = await bookReviewFacade.CreateBookReview(createBookReview);

            Assert.NotNull(result);
            Assert.Equal(bookReview.Id, result.Id);
            Assert.Equal(bookReview.Rating, result.Rating);
            await _bookReviewServiceMock.Received(1).CreateAsync((Arg.Any<BookReview>()));
            await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task CreateReviewForNonExistingBook_ShouldThrowExceptionBookDoesNotExist()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);
        var user = TestDataInitializer.GetTestUsers().First(x => x.Id == bookReview.ReviewerId);

        var createBookReview = new CreateBookReviewDto()
        {
            BookId = bookReview.BookId,
            ReviewerId = bookReview.ReviewerId,
            Description = bookReview.Description,
            Rating = bookReview.Rating
        };

        _bookReviewServiceMock.CreateAsync(Arg.Any<BookReview>()).Returns(bookReview);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(Book), createBookReview.BookId));
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await bookReviewFacade.CreateBookReview(createBookReview));
        }
    }

    [Fact]
    public async Task CreateReviewForNonExistingUser_ShouldThrowExceptionUserDoesNotExist()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);
        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == bookReview.BookId);

        var createBookReview = new CreateBookReviewDto()
        {
            BookId = bookReview.BookId,
            ReviewerId = bookReview.ReviewerId,
            Description = bookReview.Description,
            Rating = bookReview.Rating
        };

        _bookReviewServiceMock.CreateAsync(Arg.Any<BookReview>()).Returns(bookReview);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(User), createBookReview.ReviewerId));

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await bookReviewFacade.CreateBookReview(createBookReview));
        }
    }

    [Fact]
    public async Task UpdateReview_ShouldReturnUpdatedBookReview()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);

        var updateReviewDto = new UpdateBookReviewDto()
        {
            Rating = bookReview.Rating,
            Description = bookReview.Description
        };

        _bookReviewServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(bookReview);
        _bookReviewServiceMock.UpdateAsync(Arg.Any<BookReview>()).Returns(bookReview);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();

            var result = await bookReviewFacade.UpdateBookReview(bookReview.Id, updateReviewDto);

            Assert.NotNull(result);
            Assert.Equal(bookReview.Id, result.Id);
            Assert.Equal(bookReview.Rating, result.Rating);
            await _bookReviewServiceMock.Received(1).UpdateAsync(Arg.Any<BookReview>());
            await _bookReviewServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task UpdateNonExistingReview_ShouldThrowExceptionBookReviewDoesNotExist()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);

        var updateReviewDto = new UpdateBookReviewDto()
        {
            Rating = bookReview.Rating,
            Description = bookReview.Description
        };

        _bookReviewServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(BookReview), bookReview.Id));
        _bookReviewServiceMock.UpdateAsync(Arg.Any<BookReview>()).Returns(bookReview);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await bookReviewFacade.UpdateBookReview(1, updateReviewDto));
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]

    public async Task FindBookReviews_ShouldReturnExistingBookReviews(long bookId)
    {
        var reviews = TestDataInitializer.GetTestBookReviews().Where(x => x.BookId == bookId).ToList();

        _bookReviewServiceMock.FindByBookIdAsync(Arg.Any<long>()).Returns(reviews);

        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();

            var result = await bookReviewFacade.FindBookReviewsAsync(bookId);

            Assert.NotNull(result);
            Assert.Equal(reviews.Count, result.Count);
            Assert.All(result, item => Assert.Equal(bookId, item.BookId));
        }
     }

    [Fact]
    public async Task FindUserReviews_ShouldReturnExistingBookReviewsByUserId()
    {
        long userId = 3;
        var reviews = TestDataInitializer.GetTestBookReviews().Where(x => x.ReviewerId == userId).ToList();

        _bookReviewServiceMock.FindByUserIdAsync(Arg.Any<long>()).Returns(reviews);

        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewFacade = scope.ServiceProvider.GetRequiredService<IBookReviewFacade>();

            var result = await bookReviewFacade.FindUserReviewsAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(reviews.Count, result.Count);
            Assert.All(result, item => Assert.Equal(userId, item.ReviewerId));
        }
    }
}
