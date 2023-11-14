using AutoMapper;
using BookHubWebAPI.Api.Address.Create;
using BookHubWebAPI.Api.Address.View;
using DataAccessLayer.Models.Logistics;

namespace BookHubWebAPI.Mappers;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<Address, DetailedAddressView>();
    }
}