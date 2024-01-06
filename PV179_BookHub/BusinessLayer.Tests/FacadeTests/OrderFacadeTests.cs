using BusinessLayer.Services.Order;
using BusinessLayer.Services;
using TestUtilities.MockedObjects;
using DataAccessLayer.Models.Purchasing;
using DataAccessLayer.Models.Account;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedData;
using BusinessLayer.Facades.Order;
using BusinessLayer.Services.InventoryItem;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models.Enums;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.Services.Book;

namespace BusinessLayer.Tests.FacadeTests;

public class OrderFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IOrderService _orderServiceMock;
    private IGenericService<OrderItem, long> _orderItemServiceMock;
    private IInventoryItemService _inventoryItemServiceMock;
    private IGenericService<User, long> _userServiceMock;
    private IBookService _bookServiceMock;

    public OrderFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _orderServiceMock = Substitute.For<IOrderService>();
        _orderItemServiceMock = Substitute.For<IGenericService<OrderItem, long>>();
        _inventoryItemServiceMock = Substitute.For<IInventoryItemService>();
        _userServiceMock = Substitute.For<IGenericService<User, long>>();
        _bookServiceMock = Substitute.For<IBookService>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_orderServiceMock)
            .AddScoped(_orderItemServiceMock)
            .AddScoped(_inventoryItemServiceMock)
            .AddScoped(_userServiceMock)
            .AddScoped(_bookServiceMock)
            .Create();
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnNewOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);
        var user = TestDataInitializer.GetTestUsers().First(x => x.Id == order.UserId);

        _orderServiceMock.CreateAsync(Arg.Any<Order>(),Arg.Any<bool>()).Returns(order);
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.CreateOrderAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal(order.State, result.State);
            Assert.Equal(order.Id, result.Id);

            await _orderServiceMock.Received(1).CreateAsync((Arg.Any<Order>()));
            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
        await _orderServiceMock.Received(1).CreateAsync((Arg.Any<Order>()));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task FetchOrdersByUserIdAsync_ShouldReturnOrdersByUserId(long userId)
    {
        var order = TestDataInitializer.GetTestOrderList().Where(x => x.UserId == userId);
        _orderServiceMock.FetchAllByUserIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.FetchOrdersByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(new List<GeneralOrderViewDto>(), result);
            await _orderServiceMock.Received(1).FetchAllByUserIdAsync(Arg.Any<long>());
        }
    }

    [Theory]
    [InlineData(3)]
    public async Task FetchOrdersByUserIdAsync_ShouldReturnMultipleOrdersByUserId(long userId)
    {
        var order = TestDataInitializer.GetTestOrderList().Where(x => x.UserId == userId);
        _orderServiceMock.FetchAllByUserIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.FetchOrdersByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, item => Assert.Equal(userId, item.UserId));
            await _orderServiceMock.Received(1).FetchAllByUserIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task FindOrderByIdAsync_ShouldReturnExistingOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);
        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.FindOrderByIdAsync(order.Id);

            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.State, result.State);
            await _orderServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task PayForOrderAsync_ShouldThrowExceptionCannotPayForOrderWrongState()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();
            await Assert.ThrowsAnyAsync<WrongOrderStateException>(
                async () => await orderFacade.PayForOrderAsync(order.Id)
            );
        }
    }

    [Fact]
    public async Task PayForOrderAsync_ShouldReturnPaidOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);
        order.State = OrderState.Created;

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();
            
            var result = await orderFacade.PayForOrderAsync(order.Id);
            Assert.NotNull(result);
            Assert.Equal(OrderState.Paid, result.State);

            await _orderServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task RefundOrderAsync_ShouldThrowExceptionWrongOrderState()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();
            await Assert.ThrowsAnyAsync<WrongOrderStateException>(
                async () => await orderFacade.RefundOrderAsync(order.Id)
            );
        }
    }

    [Fact]
    public async Task RefundOrderAsync_ShouldReturnRefundedOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);
        order.State = OrderState.Paid;

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.RefundOrderAsync(order.Id);
            Assert.NotNull(result);
            Assert.Equal(OrderState.Refunded, result.State);

            await _orderServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldThrowExceptionCannotRefundOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();
            await Assert.ThrowsAnyAsync<WrongOrderStateException>(
                async () => await orderFacade.RefundOrderAsync(order.Id)
            );
        }
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReturnCancelledOrder()
    {
        var order = TestDataInitializer.GetTestOrderList().ElementAt(0);
        order.State = OrderState.Created;

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.CancelOrderAsync(order.Id);
            Assert.NotNull(result);
            Assert.Equal(OrderState.Cancelled, result.State);

            await _orderServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task CreateOrderItem_ShouldThrowExceptionCannotCreateOrderItem()
    {
        var orderItem = TestDataInitializer.GetTestOrderItems().ElementAt(0);

        var createOrderItemDto = new CreateOrderItemDto
        {
            BookId = orderItem.BookId,
            BookStoreId = orderItem.BookStoreId,
            Quantity = orderItem.Quantity,
        };

        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == orderItem.BookId);
        var order = TestDataInitializer.GetTestOrderList().First(x => x.Id == orderItem.OrderId);

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            await Assert.ThrowsAnyAsync<WrongOrderStateException>(
                async () => await orderFacade.CreateOrderItem(order.Id, createOrderItemDto)
            );
        }
    }

    [Fact]
    public async Task CreateOrderItem_ShouldReturnOrderWithNewOrderItem()
    {
        var orderItem = TestDataInitializer.GetTestOrderItems().ElementAt(0);

        var createOrderItemDto = new CreateOrderItemDto
        {
            BookId = orderItem.BookId,
            BookStoreId = orderItem.BookStoreId,
            Quantity = orderItem.Quantity,
        };

        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == orderItem.BookId);
        var order = TestDataInitializer.GetTestOrderList().First(x => x.Id == orderItem.OrderId);
        order.State = OrderState.Created;

        _orderServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(order);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);
        _orderItemServiceMock.CreateAsync(Arg.Any<OrderItem>()).Returns(orderItem);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.CreateOrderItem(order.Id, createOrderItemDto);

            Assert.NotNull(result);
            Assert.Equal(orderItem.Id, result.Id);
            Assert.Equal(orderItem.Quantity, result.Quantity);
            Assert.Equal(orderItem.Price, result.Price);
            await _orderServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task FindOrderItemByIdAsync_ShouldReturnExistingOrderItem()
    {
        var orderItem = TestDataInitializer.GetTestOrderItems().ElementAt(0);
        _orderItemServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(orderItem);

        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == orderItem.BookId);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);
            
        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var orderFacade = scope.ServiceProvider.GetRequiredService<IOrderFacade>();

            var result = await orderFacade.FindOrderItemByIdAsync(orderItem.Id);

            Assert.NotNull(result);
            Assert.Equal(orderItem.Id, result.Id);
            Assert.Equal(orderItem.Quantity, result.Quantity);
            Assert.Equal(orderItem.Price, result.Price);

            await _orderItemServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }
}
