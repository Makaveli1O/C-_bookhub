using AutoMapper;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Purchasing;

namespace BusinessLayer.Mappers;

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
