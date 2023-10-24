using AutoMapper;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, DetailedOrderViewDto>();
    }
}
