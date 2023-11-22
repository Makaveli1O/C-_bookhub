using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;

namespace BusinessLayer.Services;

public interface IAddressService : IBaseService
{
    Task<bool> DeleteAddressByIdAsync(long id);
    Task<DetailedAddressView?> FindAddressByIdAsync(long id);
    Task<DetailedAddressView?> CreateAddressAsync(CreateAddressDto createAddressDto);
    Task<DetailedAddressView?> UpdateAddressAsync(long id, CreateAddressDto createAddressDto);

}
