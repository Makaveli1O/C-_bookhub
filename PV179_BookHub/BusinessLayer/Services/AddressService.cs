using AutoMapper;
using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;
using DataAccessLayer.Models;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services;

public class AddressService : BaseService, IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddressService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DetailedAddressView?> FindAddressByIdAsync(long id)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);

        return _mapper.Map<DetailedAddressView>(address);
    }

    public async Task<bool> DeleteAddressByIdAsync(long id)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);

        if (address != null)
        {
            _unitOfWork.AddressRepository.Delete(address);
            await _unitOfWork.CommitAsync();

            return true;
        }

        return false;
    }

    public async Task<DetailedAddressView?> CreateAddressAync(CreateAddressDto createAddressDto)
    {
        var address = _mapper.Map<Address>(createAddressDto);

        await _unitOfWork.AddressRepository.AddAsync(address);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<DetailedAddressView>(address);
    }

    public async Task<DetailedAddressView?> UpdateAddressAsync(long id, CreateAddressDto createAddressDto)
    {
        var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);

        if (address != null)
        {
            address.StreetNumber = createAddressDto.StreetNumber ?? address.StreetNumber;
            address.City = createAddressDto.City ?? address.City;
            address.State = createAddressDto.State ?? address.State;
            address.Street = createAddressDto.Street ?? address.Street;
            address.PostalCode = createAddressDto.PostalCode ?? address.PostalCode;

            _unitOfWork.AddressRepository.Update(address);
            await _unitOfWork.CommitAsync();
        }
        return _mapper.Map<DetailedAddressView>(address);
    }

}
