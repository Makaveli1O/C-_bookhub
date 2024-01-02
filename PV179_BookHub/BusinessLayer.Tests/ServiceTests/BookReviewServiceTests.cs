using BusinessLayer.Exceptions;
using BusinessLayer.Services.BookReview;
using DataAccessLayer.Models.Account;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using System.Linq.Expressions;
using TestUtilities.MockedData;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.ServiceTests;

public class BookReviewServiceTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IGenericRepository<BookReview, long> _repositoryMock;
    private IUnitOfWork _uowMock;

    public BookReviewServiceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddUnitOfWork()
            .AddRepositories()
            .AddServices()
            .AddMockedDBContext();

        _repositoryMock = Substitute.For<IGenericRepository<BookReview, long>>();
        _uowMock = Substitute.For<IUnitOfWork>();
        _uowMock.GetRepositoryByEntity<BookReview, long>().Returns(_repositoryMock);
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_repositoryMock)
            .AddScoped(_uowMock)
            .Create();
    }

    [Fact]
    public async Task CreateBookReviewAsync_ShouldReturnNewBookReview()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.CreateAsync(bookReview);

            Assert.NotNull(result);
            Assert.Equal(result.Id, bookReview.Id);
            Assert.Equal(result.Rating, bookReview.Rating);
            await _repositoryMock.Received(1).AddAsync(Arg.Any<BookReview>());
        }
    }

    [Fact]
    public async Task UpdateBookReviewAsync_ShouldReturnUpdatedBookReview()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);
        bookReview.Rating = DataAccessLayer.Models.Enums.Rating.Fair;

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.UpdateAsync(bookReview);

            Assert.NotNull(result);
            Assert.Equal(result.Id, bookReview.Id);
            Assert.Equal(result.Description, bookReview.Description);
            _repositoryMock.Received(1).Update(Arg.Any<BookReview>());
        }
    }

    [Fact]
    public async Task FetchAllBookReviews_ShouldReturnAllReviews()
    {
        var reviews = TestDataInitializer.GetTestBookReviews();

        _repositoryMock.GetAllAsync().Returns(reviews);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.FetchAllAsync();

            Assert.NotNull(result);
            Assert.Equal(result.Count(), reviews.Count);
            Assert.All(result, x => Assert.Contains(x.Id, reviews.Select(x => x.Id)));
            await _repositoryMock.Received(1).GetAllAsync();
        }

    }

    [Fact]
    public async Task FetchAllBookReviews_ShouldReturnEmptyListOfReviews()
    {
        _repositoryMock.GetAllAsync().Returns(new List<BookReview>());

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.FetchAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
            await _repositoryMock.Received(1).GetAllAsync();
        }
    }

    [Fact]
    public async Task GetBookReviewAsync_ShouldReturnExistingBookReview()
    {
        var bookReview = TestDataInitializer.GetTestBookReviews().ElementAt(0);

        _repositoryMock.GetByIdAsync(Arg.Any<long>()).Returns(bookReview);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.FindByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(bookReview.Id, result.Id);
            Assert.Equal(bookReview.Rating, result.Rating);
            await _repositoryMock.Received(1).GetByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task GetBookReviewAsync_ShouldThrowExceptionBookReviewDoesNotExist()
    {
        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await bookReviewService.FindByIdAsync(1));
        }
    }

    [Fact]
    public async Task FindByBookIdAsync_ShouldReturnReviewsByBookId()
    {
        long bookId = 2;
        var bookReviews = TestDataInitializer.GetTestBookReviews().Where(x => x.BookId == bookId);

        _repositoryMock.GetAllAsync(Arg.Any<Expression<Func<BookReview, bool>>>()).Returns(bookReviews);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.FindByBookIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(result.Count(), bookReviews.Count());
            Assert.All(result, x => Assert.Equal(bookId, x.BookId));
            await _repositoryMock.Received(1).GetAllAsync(Arg.Any<Expression<Func<BookReview, bool>>>());
        }
    }

    [Fact]
    public async Task FindByUserIdAsync_ShouldReturnReviewsByUserId()
    {
        long userId = 3;
        var bookReviews = TestDataInitializer.GetTestBookReviews().Where(x => x.ReviewerId == userId);

        _repositoryMock.GetAllAsync(Arg.Any<Expression<Func<BookReview, bool>>>()).Returns(bookReviews);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookReviewService = scope.ServiceProvider.GetRequiredService<IBookReviewService>();

            var result = await bookReviewService.FindByBookIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal(result.Count(), bookReviews.Count());
            Assert.All(result, x => Assert.Equal(userId, x.ReviewerId));
            await _repositoryMock.Received(1).GetAllAsync(Arg.Any<Expression<Func<BookReview, bool>>>());
        }
    }
}
