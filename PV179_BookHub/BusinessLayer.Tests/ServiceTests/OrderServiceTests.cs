using NSubstitute;
using TestUtilities.MockedObjects;
using Infrastructure.Repository;
using DataAccessLayer.Models.Purchasing;
using BusinessLayer.Services.Order;
using Microsoft.Extensions.DependencyInjection;
using DataAccessLayer.Models.Enums;
using System.Linq.Expressions;
using TestUtilities.MockedData;
using DataAccessLayer.Models.Account;
using Infrastructure.UnitOfWork;
using BusinessLayer.Services.BookReview;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models.Publication;

namespace BusinessLayer.Tests.ServiceTests;

public class OrderServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private readonly IGenericRepository<Order, long> _repositoryMock;
    private IUnitOfWork _uowMock;

    public OrderServiceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddUnitOfWork()
            .AddRepositories()
            .AddServices()
            .AddMockedDBContext();


        _serviceProviderBuilder.AddServices().Create();

        _repositoryMock = Substitute.For<IGenericRepository<Order, long>>();
        _uowMock = Substitute.For<IUnitOfWork>();
        _uowMock.GetRepositoryByEntity<Order, long>().Returns(_repositoryMock);

    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_repositoryMock)
            .AddScoped(_uowMock)
            .Create();
    }


    [Fact]
    public async Task FetchAllOrders_ShouldReturnEmptyList()
    {
        _repositoryMock.GetAllAsync().Returns(new List<Order>());

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderservice = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var result = await orderservice.FetchAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
            await _repositoryMock.Received(1).GetAllAsync();
        }
    }


    [Fact]
    public async Task CreateOrderAsync_ShouldReturnNewOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var result = await orderService.CreateAsync(order);

            Assert.NotNull(result);
            Assert.Equal(result.Id, order.Id);
            Assert.Equal(result.State, order.State);
            await _repositoryMock.Received(1).AddAsync(Arg.Any<Order>());
        }
    }


    [Fact]
    public async Task GetOrderAsync_ShouldThrowException()
    {
        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await orderService.FindByIdAsync(1));
        }
    }

    [Fact]
    public async Task CheckForActiveOrdersByUserIdAsync_ShoudReturnFalse()
    {
        var order = TestDataInitializer.GetTestUsers().ElementAt(1);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var result = await orderService.CheckForActiveOrdersByUserIdAsync(order.Id);

            Assert.False(result);
            await _repositoryMock.Received(1).GetAllAsync(Arg.Any<Expression<Func<Order, bool>>>());
        }
    }

    [Fact]
    public async Task CheckForActiveOrdersByUserIdAsync_ShoudReturnTrue()
    {
        var user = TestDataInitializer.GetTestUsers().ElementAt(2);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var result = await orderService.CheckForActiveOrdersByUserIdAsync(user.Id);

            Assert.False(result);
            await _repositoryMock.Received(1).GetAllAsync(Arg.Any<Expression<Func<Order, bool>>>());
        }
    }

    [Fact]
    public async Task FetchAllByUserIdAsync_ShouldReturnEmpty()
    {
        long userId = 1;
        var orders = TestDataInitializer.GetTestOrderList().Where(x => x.UserId == userId);

        _repositoryMock.GetAllAsync(
            Arg.Any<Expression<Func<Order, bool>>>(),
            Arg.Any<Expression<Func<Order, object>>[]>()
            )
            .Returns(orders);


        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var result = await orderService.FetchAllByUserIdAsync(1);

            Assert.NotNull(result);
            Assert.Empty(result);
            await _repositoryMock.Received(1).GetAllAsync(
                Arg.Any<Expression<Func<Order,
                bool>>>(), Arg.Any<Expression<Func<Order, object>>[]>()
            );
        }
    }

    [Fact]
    public async Task FetchAllByUserIdAsync_ShouldReturnOrders()
    {
        long userId = 3;
        var orders = TestDataInitializer.GetTestOrderList().Where(x => x.UserId == userId);

        _repositoryMock.GetAllAsync(
            Arg.Any<Expression<Func<Order, bool>>>(),
            Arg.Any<Expression<Func<Order, object>>[]>()
            )
            .Returns(orders);


        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var result = await orderService.FetchAllByUserIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            await _repositoryMock.Received(1).GetAllAsync(
                Arg.Any<Expression<Func<Order,
                bool>>>(), Arg.Any<Expression<Func<Order, object>>[]>()
            );
        }
    }
}
