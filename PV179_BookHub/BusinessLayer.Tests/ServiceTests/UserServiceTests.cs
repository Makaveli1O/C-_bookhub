using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using DataAccessLayer.Models.Account;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.MockedData;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.ServiceTests;

public class UserServiceTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private IGenericRepository<User, long> _repositoryMock;
    private IUnitOfWork _uowMock;

    public UserServiceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();

        _repositoryMock = Substitute.For<IGenericRepository<User, long>>();
        _uowMock = Substitute.For<IUnitOfWork>();
        _uowMock.GetRepositoryByEntity<User, long>().Returns(_repositoryMock);
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_repositoryMock)
            .AddScoped(_uowMock)
            .Create();
    }

    [Fact]
    public async Task CreateUserAsync_ShouldReturnNewUser()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IGenericService<User, long>>();

            var result = await userService.CreateAsync(user);

            Assert.NotNull(result);
            Assert.Equal(result.Id, user.Id);
            Assert.Equal(user.UserName, result.UserName);
            await _repositoryMock.Received(1).AddAsync(Arg.Any<User>());
        }
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldReturnUpdatedUser()
    {
        var user = TestDataInitializer.GetTestUsers().First();
        user.UserName = user.UserName.ToUpper();

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IGenericService<User, long>>();

            var result = await userService.UpdateAsync(user);

            Assert.NotNull(result);
            Assert.Equal(result.Id, user.Id);
            Assert.Equal(user.UserName, result.UserName);
            _repositoryMock.Received(1).Update(Arg.Any<User>());
        }
    }

    [Fact]
    public async Task FetchAllUsers_ShouldReturnAllUsers()
    {
        var users = TestDataInitializer.GetTestUsers();

        _repositoryMock.GetAllAsync().Returns(users);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IGenericService<User, long>>();

            var result = await userService.FetchAllAsync();

            Assert.NotNull(result);
            Assert.Equal(result.Count(), users.Count);
            Assert.All(result, x => Assert.Contains(x.Id, users.Select(x => x.Id)));
            await _repositoryMock.Received(1).GetAllAsync();
        }

    }

    [Fact]
    public async Task FetchAllUsers_ShouldReturnEmptyListOfUsers()
    {
        _repositoryMock.GetAllAsync().Returns(new List<User>());

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IGenericService<User, long>>();

            var result = await userService.FetchAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
            await _repositoryMock.Received(1).GetAllAsync();
        }
    }

    [Fact]
    public async Task GetUserAsync_ShouldReturnExistingUser()
    {
        var user = TestDataInitializer.GetTestUsers().First();

        _repositoryMock.GetByIdAsync(Arg.Any<long>()).Returns(user);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IGenericService<User, long>>();

            var result = await userService.FindByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            await _repositoryMock.Received(1).GetByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task GetUserAsync_ShouldThrowExceptionUserDoesNotExist()
    {
        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IGenericService<User, long>>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await userService.FindByIdAsync(1));
        }
    }
}
