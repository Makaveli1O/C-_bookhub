using AutoMapper;
using BookHubWebAPI.Api.Order.Create;
using BookHubWebAPI.Api.Order.View;
using DataAccessLayer.Models.Purchasing;

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
