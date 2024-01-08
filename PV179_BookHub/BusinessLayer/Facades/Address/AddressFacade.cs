using AutoMapper;
using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;
using BusinessLayer.Services;

namespace BusinessLayer.Facades.Address;

public class AddressFacade : BaseFacade, IAddressFacade
{
    private readonly IGenericService<AddressEntity, long> _addressService;
    public AddressFacade(IMapper mapper, IGenericService<AddressEntity, long> addressService) : base(mapper, null, null)
    {
        _addressService = addressService;
    }

    public async Task<DetailedAddressView> CreateAddressAsync(CreateAddressDto createAddressDto)
    {
        var address = _mapper.Map<AddressEntity>(createAddressDto);
        var createdAddress = await _addressService.CreateAsync(address);

        return _mapper.Map<DetailedAddressView>(createdAddress);
    }

    public async Task DeleteAddressByIdAsync(long id)
    {
        var address = await _addressService.FindByIdAsync(id);
        await _addressService.DeleteAsync(address);
    }

    public async Task<DetailedAddressView> FindAddressByIdAsync(long id)
    {
        return _mapper.Map<DetailedAddressView>(await _addressService.FindByIdAsync(id));
    }

    public async Task<DetailedAddressView> UpdateAddressAsync(long id, CreateAddressDto createAddressDto)
    {
        var address = await _addressService.FindByIdAsync(id);

        address.StreetNumber = createAddressDto.StreetNumber ?? address.StreetNumber;
        address.City = createAddressDto.City ?? address.City;
        address.State = createAddressDto.State ?? address.State;
        address.Street = createAddressDto.Street ?? address.Street;
        address.PostalCode = createAddressDto.PostalCode ?? address.PostalCode;

        var updatedAddress = await _addressService.UpdateAsync(address);
        return _mapper.Map<DetailedAddressView>(updatedAddress);
    }
}
