using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;

namespace BusinessLayer.Facades.Address;

public interface IAddressFacade
{
    Task<DetailedAddressView> FindAddressByIdAsync(long id);
    Task DeleteAddressByIdAsync(long id);
    Task<DetailedAddressView> CreateAddressAsync(CreateAddressDto createAddressDto);
    Task<DetailedAddressView> UpdateAddressAsync(long id, CreateAddressDto createAddressDto);
}
