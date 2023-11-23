using AutoMapper;
using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Address;

public class AddressService : BaseService<DataAccessLayer.Models.Logistics.Address, long>, IAddressService
{
    public AddressService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public async Task<DetailedAddressView?> FindAddressByIdAsync(long id)
    {
        var address = await Repository.GetByIdAsync(id);

        return _mapper.Map<DetailedAddressView>(address);
    }

    public async Task<bool> DeleteAddressByIdAsync(long id)
    {
        var address = await Repository.GetByIdAsync(id);

        if (address != null)
        {
            Repository.Delete(address);
            await SaveAsync();

            return true;
        }

        return false;
    }

    public async Task<DetailedAddressView?> CreateAddressAsync(CreateAddressDto createAddressDto)
    {
        var address = _mapper.Map<DataAccessLayer.Models.Logistics.Address>(createAddressDto);

        await Repository.AddAsync(address);
        await SaveAsync();

        return _mapper.Map<DetailedAddressView>(address);
    }

    public async Task<DetailedAddressView?> UpdateAddressAsync(long id, CreateAddressDto createAddressDto)
    {
        var address = await Repository.GetByIdAsync(id);

        if (address != null)
        {
            address.StreetNumber = createAddressDto.StreetNumber ?? address.StreetNumber;
            address.City = createAddressDto.City ?? address.City;
            address.State = createAddressDto.State ?? address.State;
            address.Street = createAddressDto.Street ?? address.Street;
            address.PostalCode = createAddressDto.PostalCode ?? address.PostalCode;

            Repository.Update(address);
            await SaveAsync();
        }
        return _mapper.Map<DetailedAddressView>(address);
    }

}
