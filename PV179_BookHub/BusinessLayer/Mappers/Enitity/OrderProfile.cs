using AutoMapper;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Purchasing;

namespace BusinessLayer.Mappers.Enitity;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderEntity, DetailedOrderViewDto>();
        CreateMap<OrderEntity, GeneralOrderViewDto>();

        CreateMap<CreateOrderItemDto, OrderItemEntity>();
        CreateMap<OrderItemEntity, DetailedOrderItemViewDto>();
        CreateMap<OrderItemEntity, GeneralOrderItemViewDto>();
    }
}
