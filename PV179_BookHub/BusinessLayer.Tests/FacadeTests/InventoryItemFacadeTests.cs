using BusinessLayer.Services.Order;
using BusinessLayer.Services;
using DataAccessLayer.Models.Account;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUtilities.MockedObjects;
using DataAccessLayer.Models.Logistics;
using TestUtilities.MockedData;
using BusinessLayer.Facades.BookStore;
using BusinessLayer.DTOs.BookStore.Create;
using EntityFrameworkCore.Testing.NSubstitute;
using BusinessLayer.Services.Book;
using BusinessLayer.Services.InventoryItem;

namespace BusinessLayer.Tests.FacadeTests;

public class InventoryItemFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IGenericService<BookStore, long> _bookStoreServiceMock;
    private IInventoryItemService _inventoryItemServiceMock;
    private IBookService _bookServiceMock;

    public InventoryItemFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddUnitOfWork()
            .AddRepositories()
            .AddAutoMapper()
            .AddServices()
            .AddFacades()
            .AddMockedDBContext();

        _bookStoreServiceMock = Substitute.For<IGenericService<BookStore, long>>();
        _bookServiceMock = Substitute.For<IBookService>();
        _inventoryItemServiceMock = Substitute.For<IInventoryItemService>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_bookStoreServiceMock)
            .AddScoped(_bookServiceMock)
            .AddScoped(_inventoryItemServiceMock)
            .Create();
    }
    
    [Fact]
    public async Task GetAllInventoryItems_ShouldReturnAllInventoryItems()
    {
        var inventoryItems = TestDataInitializer.GetTestInventoryItems();
        _inventoryItemServiceMock.FetchAllAsync().Returns(inventoryItems);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var inventoryItemFacade = scope.ServiceProvider.GetRequiredService<IInventoryItemFacade>();

            var result = await inventoryItemFacade.GetAllInventoryItems();

            Assert.NotNull(result);
            Assert.Equal(6, result.Count());
            await _inventoryItemServiceMock.Received(1).FetchAllAsync();
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetInventoryItem_ShouldReturnExistingInventoryItem(long inventoryItemId)
    {
        var inventoryItem = TestDataInitializer.GetTestInventoryItems().First(x => x.Id == inventoryItemId);
        _inventoryItemServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(inventoryItem);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var inventoryItemFacade = scope.ServiceProvider.GetRequiredService<IInventoryItemFacade>();

            var result = await inventoryItemFacade.GetInventoryItem(inventoryItemId);

            Assert.NotNull(result);
            Assert.Equal(inventoryItemId, result.Id);
            await _inventoryItemServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task CreateInventoryItem_ShouldCreateNewInventoryItem()
    {
        var inventoryItem = TestDataInitializer.GetTestInventoryItems().ElementAt(0);
        _inventoryItemServiceMock.CreateAsync(Arg.Any<InventoryItem>()).Returns(inventoryItem);

        var createInventoryItemDto = new CreateInventoryItemDto
        {
            BookId = inventoryItem.BookId,
            BookStoreId = inventoryItem.BookStoreId,
            InStock = inventoryItem.InStock,
            LastRestock = inventoryItem.LastRestock,
        };

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var inventoryItemFacade = scope.ServiceProvider.GetRequiredService<IInventoryItemFacade>();

            var result = await inventoryItemFacade.CreateInventoryItem(createInventoryItemDto);

            Assert.NotNull(result);
            Assert.Equal(inventoryItem.Id, result.Id);
            await _inventoryItemServiceMock.Received(1).CreateAsync(Arg.Any<InventoryItem>());
        }
    }
    
    [Fact]
    public async Task UpdateBookStore_ShouldUpdateExistingInventoryItem()
    {
        var inventoryItem = TestDataInitializer.GetTestInventoryItems().ElementAt(0);
        _inventoryItemServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(inventoryItem);
        _inventoryItemServiceMock.UpdateAsync(Arg.Any<InventoryItem>()).Returns(inventoryItem);

        var book = TestDataInitializer.GetTestBooks().First(x => x.Id == inventoryItem.BookId);
        _bookServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(book);

        var bookStore = TestDataInitializer.GetTestBookStores().First(x => x.Id == inventoryItem.BookStoreId);
        _bookStoreServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(bookStore);

        var createInventoryItemDto = new CreateInventoryItemDto
        {
            BookId = inventoryItem.BookId,
            BookStoreId = inventoryItem.BookStoreId,
            InStock = inventoryItem.InStock,
            LastRestock = inventoryItem.LastRestock
        };

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var inventoryItemFacade = scope.ServiceProvider.GetRequiredService<IInventoryItemFacade>();

            var result = await inventoryItemFacade.UpdateInventoryItem(inventoryItem.Id, createInventoryItemDto);

            Assert.NotNull(result);
            Assert.Equal(inventoryItem.Id, result.Id);
            Assert.Equal(inventoryItem.BookStoreId, result.BookStoreId);
            Assert.Equal(inventoryItem.InStock, result.InStock);

            await _inventoryItemServiceMock.Received(1).UpdateAsync(Arg.Any<InventoryItem>());
            await _inventoryItemServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _bookServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _bookStoreServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }
}
