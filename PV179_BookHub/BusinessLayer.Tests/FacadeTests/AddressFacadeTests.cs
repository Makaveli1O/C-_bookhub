using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.MockedData;
using TestUtilities.MockedObjects;
using DataAccessLayer.Models.Logistics;
using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.Facades.Address;
using NSubstitute.ExceptionExtensions;

namespace BusinessLayer.Tests.FacadeTests;

public class AddressFacadeTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;
    private readonly IGenericService<Address, long> _addressServiceMock;

    public AddressFacadeTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
            .AddInfrastructure()
            .AddBusinessLayer();
        _addressServiceMock = Substitute.For<IGenericService<Address, long>>();
    }

    private ServiceProvider CreateServiceProvider()
    {
        return _serviceProviderBuilder
            .AddScoped(_addressServiceMock)
            .Create();
    }

    [Fact]
    public async Task CreateAddress_ShouldReturnNewAddress()
    {
        var address = TestDataInitializer.GetTestAddresses().ElementAt(0);

        var addressCreateDto = new CreateAddressDto()
        {
            City = address.City,
            PostalCode = address.PostalCode,
            State = address.State,
        };


        _addressServiceMock.CreateAsync(Arg.Any<Address>()).Returns(address);
        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var addressFacade = scope.ServiceProvider.GetRequiredService<IAddressFacade>();
            var result = await addressFacade.CreateAddressAsync(addressCreateDto);

            Assert.NotNull(result);
            Assert.Equal(address.Id, result.Id);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.PostalCode, result.PostalCode);
            Assert.Equal(address.State, result.State);
            await _addressServiceMock.Received(1).CreateAsync((Arg.Any<Address>()));
        }
    }

    [Fact]
    public async Task UpdateAddress_ShouldReturnUpdatedAddress()
    {
        var address = TestDataInitializer.GetTestAddresses().ElementAt(0);
        var addressDto = new CreateAddressDto()
        {
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
        };

        _addressServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(address);
        _addressServiceMock.UpdateAsync(Arg.Any<Address>()).Returns(address);

        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var addressFacade = scope.ServiceProvider.GetRequiredService<IAddressFacade>();
            var result = await addressFacade.UpdateAddressAsync(address.Id, addressDto);

            Assert.NotNull(result);
            Assert.Equal(address.Id, result.Id);
            Assert.Equal(address.Street, result.Street);
            Assert.Equal(address.State, result.State);
            await _addressServiceMock.Received(1).UpdateAsync((Arg.Any<Address>()));
            await _addressServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }

    [Fact]
    public async Task UpdateNotExistingAddress_ShouldThrowExceptionAddressDoesNotExist()
    {
        var address = TestDataInitializer.GetTestAddresses().ElementAt(0);

        var addressDto = new CreateAddressDto()
        {
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
        };

        _addressServiceMock.FindByIdAsync(Arg.Any<long>()).Throws(new NoSuchEntityException<long>(typeof(Address), address.Id));
        var serviceProvider = CreateServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var addressFacade = scope.ServiceProvider.GetRequiredService<IAddressFacade>();
            await Assert.ThrowsAnyAsync<NoSuchEntityException<long>>(async () => await addressFacade.UpdateAddressAsync(address.Id, addressDto));
            await _addressServiceMock.DidNotReceive().UpdateAsync(Arg.Any<Address>());
        }
    }

    [Fact]
    public async Task FindAddress_ShouldReturnExistingAddress()
    {
        long id = 1;
        var address = TestDataInitializer.GetTestAddresses().First(x => x.Id == id);
        _addressServiceMock.FindByIdAsync(Arg.Any<long>()).Returns(address);

        var serviceProvider = CreateServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var addressFacade = scope.ServiceProvider.GetRequiredService<IAddressFacade>();
            var result = await addressFacade.FindAddressByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(address.Id, result.Id);
            Assert.Equal(address.Street, result.Street);
            Assert.Equal(address.State, result.State);
            await _addressServiceMock.Received(1).FindByIdAsync(Arg.Any<long>());
        }
    }
}

