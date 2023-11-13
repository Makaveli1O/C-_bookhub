using AutoMapper;
using BookHubWebAPI.Api.Address.Create;
using BookHubWebAPI.Api.Address.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<Address, DetailedAddressView>();
    }
}