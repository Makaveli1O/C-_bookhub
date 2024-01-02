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

namespace BusinessLayer.Tests.FacadeTests;

public class BookStoreFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IGenericService<BookStore, long> _bookStoreServiceMock;
    private IGenericService<User, long> _userServiceMock;
    private IGenericService<Address, long> _addresServiceMock;

    public BookStoreFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddUnitOfWork()
            .AddRepositories()
            .AddAutoMapper()
            .AddServices()
            .AddFacades()
            .AddMockedDBContext();

        _bookStoreServiceMock = Substitute.For<IGenericService<BookStore, long>>();
        _userServiceMock = Substitute.For<IGenericService<User, long>>();
        _addresServiceMock = Substitute.For <IGenericService<Address, long>>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_bookStoreServiceMock)
            .AddScoped(_userServiceMock)
            .AddScoped(_addresServiceMock)
            .Create();
    }

    [Fact]
    public async Task GetAllBookStores_ShouldReturnListOfBookStores()
    {
        var bookStores = TestDataInitializer.GetTestBookStores();
        _bookStoreServiceMock.FetchAllAsync().Returns(bookStores);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookStoreFacade = scope.ServiceProvider.GetRequiredService<IBookStoreFacade>();

            var result = await bookStoreFacade.GetAllBookStores();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            await _bookStoreServiceMock.Received(1).FetchAllAsync();
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetBookStore_ShouldReturnExistingBookStore(long bookstoreId)
    {
        var bookStore = TestDataInitializer.GetTestBookStores().First(x => x.Id == bookstoreId);
        _bookStoreServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(bookStore);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookStoreFacade = scope.ServiceProvider.GetRequiredService<IBookStoreFacade>();

            var result = await bookStoreFacade.GetBookStore(bookstoreId);

            Assert.NotNull(result);
            Assert.Equal(bookstoreId, result.Id);
            await _bookStoreServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task CreateBookStore_ShouldCreateNewBookStore()
    {
        var bookStore = TestDataInitializer.GetTestBookStores().ElementAt(0);
        _bookStoreServiceMock.CreateAsync(Arg.Any<BookStore>()).Returns(bookStore);

        var createBookStoreDto = new CreateBookStoreDto
        {
            AddressId = bookStore.AddressId,
            ManagerId = bookStore.ManagerId,
            Name = bookStore.Name,
            PhoneNumber = bookStore.PhoneNumber,
            Email = bookStore.Email,
        };

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookStoreFacade = scope.ServiceProvider.GetRequiredService<IBookStoreFacade>();

            var result = await bookStoreFacade.CreateBookStore(createBookStoreDto);

            Assert.NotNull(result);
            Assert.Equal(bookStore.Id, result.Id);
            await _bookStoreServiceMock.Received(1).CreateAsync(Arg.Any<BookStore>());
        }
    }

    [Fact]
    public async Task UpdateBookStore_ShouldUpdateExistingBookStore()
    {
        var bookStore = TestDataInitializer.GetTestBookStores().ElementAt(0);
        _bookStoreServiceMock.UpdateAsync(Arg.Any<BookStore>()).Returns(bookStore);
        _bookStoreServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(bookStore);

        var address = TestDataInitializer.GetTestAddresses().First(x => x.Id == bookStore.AddressId);
        _addresServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(address);

        var user = TestDataInitializer.GetTestUsers().First(x => x.Id == bookStore.ManagerId);
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);

        var createBookStoreDto = new CreateBookStoreDto
        {
            AddressId = bookStore.AddressId,
            ManagerId = bookStore.ManagerId,
            Name = bookStore.Name + "_ChangedNameTEST",
            PhoneNumber = bookStore.PhoneNumber,
            Email = bookStore.Email,
        };

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var bookStoreFacade = scope.ServiceProvider.GetRequiredService<IBookStoreFacade>();

            var result = await bookStoreFacade.UpdateBookStore(bookStore.Id, createBookStoreDto);

            Assert.NotNull(result);
            Assert.Equal(bookStore.Id, result.Id);
            Assert.Equal(bookStore.Name, result.Name);
            Assert.Equal(bookStore.Email, result.Email);

            await _bookStoreServiceMock.Received(1).UpdateAsync(Arg.Any<BookStore>());
            await _addresServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }
}
