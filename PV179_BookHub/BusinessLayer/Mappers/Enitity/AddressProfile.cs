using AutoMapper;
using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;

namespace BusinessLayer.Mappers.Enitity;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, AddressEntity>();
        CreateMap<AddressEntity, DetailedAddressView>();
        CreateMap<AddressEntity, GeneralAddressView>();
    }
}
