using BusinessLayer.Services;
using DataAccessLayer.Models.Account;
using NSubstitute;
using TestUtilities.MockedObjects;
using BusinessLayer.Services.Order;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedData;
using BusinessLayer.DTOs.User.Create;
using BusinessLayer.Facades.User;
using BusinessLayer.Exceptions;
using NSubstitute.ExceptionExtensions;

namespace BusinessLayer.Tests.FacadeTests;

public class UserFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IOrderService _orderService;
    private IGenericService<User, long> _userServiceMock;

    public UserFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _orderService = Substitute.For<IOrderService>();
        _userServiceMock = Substitute.For<IGenericService<User, long>>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_orderService)
            .AddScoped(_userServiceMock)
            .Create();
    }

    [Fact]
    public async Task CreateUser_ShouldReturnNewUser()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        var createUser = new CreateUserDto()
        {
            UserName = user.UserName,
        };

        _userServiceMock.CreateAsync(Arg.Any<User>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();

            var result = await userFacade.CreateUserAsync(createUser);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, user.UserName);
            await _userServiceMock.Received(1).CreateAsync(Arg.Any<User>());
        }
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnUpdatedUser()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        var updateUserDto = new CreateUserDto()
        {
            UserName = user.UserName,
        };

        _userServiceMock.UpdateAsync(Arg.Any<User>()).Returns(user);
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();

            var result = await userFacade.UpdateUserAsync(user.Id, updateUserDto);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, user.UserName);
            await _userServiceMock.Received(1).UpdateAsync(Arg.Any<User>());
            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task UpdateNonExistingUser_ShouldThrowExceptionUserDoesNotExist()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        var updateUserDto = new CreateUserDto()
        {
            UserName = user.UserName,
        };

        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(User), user.Id));
        _userServiceMock.UpdateAsync(Arg.Any<User>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await userFacade.UpdateUserAsync(1, updateUserDto));
        }
    }

    [Fact]
    public async Task DeleteUserWithOutActiveOrders_ShouldReturnNothingUserShouldBeDeleted()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);
        _orderService.CheckForActiveOrdersByUserIdAsync(Arg.Any<long>()).Returns(false);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();

            await userFacade.DeleteUserAsync(user.Id);

            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _userServiceMock.Received(1).DeleteAsync(Arg.Any<User>());
            await _orderService.Received(1).CheckForActiveOrdersByUserIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task DeleteUserWithActiveOrders_ShouldThrowExceptionUserDoesNotExist()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);
        _orderService.CheckForActiveOrdersByUserIdAsync(Arg.Any<long>()).Returns(true);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();

            await Assert.ThrowsAnyAsync<RemoveErrorException>(async () => await userFacade.DeleteUserAsync(user.Id));

            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
            await _orderService.Received(1).CheckForActiveOrdersByUserIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task FetchAllUsers_ShouldReturnListOfUsers()
    {
        var users = TestDataInitializer.GetTestUsers().ToList();

        _userServiceMock.FetchAllAsync().Returns(users);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();

            var result = await userFacade.FetchAllUsersAsync();

            Assert.NotEmpty(result);
            Assert.Equal(users.Count, result.Count());
            Assert.All(result, item => Assert.Contains(item.Id, users.Select(x => x.Id)));
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task FetchSingleUser_ShouldReturnExistingUser(long userId)
    {
        var user = TestDataInitializer.GetTestUsers().First(x => x.Id == userId);

        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();

            var result = await userFacade.FetchUserAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Theory]
    [InlineData(100)]
    [InlineData(200)]
    public async Task FetchNonExistingUser_ShouldThrowExceptionUserDoesNotExist(long userId)
    {
        _userServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(User), userId));

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userFacade = scope.ServiceProvider.GetRequiredService<IUserFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await userFacade.FetchUserAsync(userId));
            await _userServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }
}
