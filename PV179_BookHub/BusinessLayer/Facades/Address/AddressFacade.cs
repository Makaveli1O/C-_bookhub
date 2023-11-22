using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;
using BusinessLayer.Services.Address;

namespace BusinessLayer.Facades.Address;

public class AddressFacade : IAddressFacade
{
    private readonly IAddressService _addressService;
    public AddressFacade(IAddressService addressService)
    {
        _addressService = addressService;
    }

    public Task<DetailedAddressView?> CreateAddressAsync(CreateAddressDto createAddressDto)
    {
        return _addressService.CreateAddressAsync(createAddressDto);
    }

    public Task<bool> DeleteAddressByIdAsync(long id)
    {
        return _addressService.DeleteAddressByIdAsync(id);
    }

    public Task<DetailedAddressView?> FindAddressByIdAsync(long id)
    {
        return _addressService.FindAddressByIdAsync(id);
    }

    public Task<DetailedAddressView?> UpdateAddressAsync(long id, CreateAddressDto createAddressDto)
    {
        return UpdateAddressAsync(id, createAddressDto);
    }
}
