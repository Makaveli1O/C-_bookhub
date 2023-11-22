using AutoMapper;
using BusinessLayer.DTOs.Address.Create;
using BusinessLayer.DTOs.Address.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Logistics;

namespace BusinessLayer.Mappers;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<Address, DetailedAddressView>();
    }
}
