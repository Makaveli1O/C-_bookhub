using AutoMapper;
using BookHubWebAPI.Api.Create;
using BookHubWebAPI.Api.View;
using DataAccessLayer.Models;

namespace BookHubWebAPI.Mappers;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, DetailedOrderViewDto>();
        CreateMap<Order, GeneralOrderViewDto>();

        CreateMap<CreateOrderItemDto, OrderItem>();
        CreateMap<OrderItem, DetailedOrderItemViewDto>();
        CreateMap<OrderItem, GeneralOrderItemViewDto>();
    }
}
