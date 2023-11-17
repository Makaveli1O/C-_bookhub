using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;

namespace BusinessLayer.Services;

public interface IAddressService : IBaseService
{
    public Task<bool> DeleteAddressByIdAsync(long id);
    public Task<DetailedAddressView?> FindAddressByIdAsync(long id);
    public Task<DetailedAddressView?> CreateAddressAync(CreateAddressDto createAddressDto);
    public Task<DetailedAddressView?> UpdateAddressAsync(long id, CreateAddressDto createAddressDto);

}
