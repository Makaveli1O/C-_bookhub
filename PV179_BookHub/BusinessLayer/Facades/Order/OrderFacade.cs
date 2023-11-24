using AutoMapper;
using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Order;
using DataAccessLayer.Models.Account;

namespace BusinessLayer.Facades.Order;

public class OrderFacade : BaseFacade, IOrderFacade
{
    private readonly IOrderService _orderService;
    public OrderFacade(IMapper mapper, IOrderService orderService) : base(mapper)
    {
        _orderService = orderService;
    }

    public async Task<DetailedOrderViewDto> CreateOrderAsync(long userId)
    {
        var order = new DataAccessLayer.Models.Purchasing.Order()
        {
            UserId = userId,
            // TODO User = await _userService.FindByIdAsync(userId);
            State = DataAccessLayer.Models.Enums.OrderState.Created,
            TotalPrice = 0
        };

        await _orderService.CreateAsync(order);

        return _mapper.Map<DetailedOrderViewDto>(order);
    }

    public Task<DetailedOrderViewDto> CreateOrderItem(long orderId, CreateOrderItemDto createOrderItemDto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteOrderByIdAsync(long id)
    {
        var order = await _orderService.FindByIdAsync(id);

        if (order.State == DataAccessLayer.Models.Enums.OrderState.Created)
        {
            // TODO return stock if possible
        }

        await _orderService.DeleteAsync(order);
    }

    public Task DeleteOrderItemByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GeneralOrderViewDto>> FetchOrdersByUserIdAsync(long userId)
    {
        //TODO await _userService.FindByIdAsync(userId);
        var orders = await _orderService.FetchAllByUserIdAsync(userId);

        return _mapper.Map<List<GeneralOrderViewDto>>(orders);
    }

    public async Task<DetailedOrderViewDto> FindOrderByIdAsync(long id)
    {
        var order = await _orderService.FindByIdAsync(id);

        return _mapper.Map<DetailedOrderViewDto>(order);
    }

    public Task<DetailedOrderItemViewDto> FindOrderItemByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    private async Task<DataAccessLayer.Models.Purchasing.Order> UpdateOrder(long id,
        DataAccessLayer.Models.Enums.OrderState requiredState,
        DataAccessLayer.Models.Enums.OrderState newState,
        bool returnStock = true)
    {
        var order = await _orderService.FindByIdAsync(id);

        if (order.State != requiredState)
        {
            throw new WrongOrderStateException(order.State, newState);
        }

        if (returnStock)
        {
            // TODO
        }
        order.State = newState;
        await _orderService.UpdateAsync(order);

        return order;
    }

    public async Task<DetailedOrderViewDto> PayForOrderAsync(long id)
    {
        return _mapper.Map<DetailedOrderViewDto>(
            await UpdateOrder(id, 
            DataAccessLayer.Models.Enums.OrderState.Created, 
            DataAccessLayer.Models.Enums.OrderState.Paid,
            false));
    }

    public async Task<DetailedOrderViewDto> RefundOrderAsync(long id)
    {
        return _mapper.Map<DetailedOrderViewDto>(
            await UpdateOrder(id, 
            DataAccessLayer.Models.Enums.OrderState.Paid,
            DataAccessLayer.Models.Enums.OrderState.Refunded));
    }

    public async Task<DetailedOrderViewDto> CancelOrderAsync(long id)
    {
        return _mapper.Map<DetailedOrderViewDto>(
            await UpdateOrder(id, 
            DataAccessLayer.Models.Enums.OrderState.Created,
            DataAccessLayer.Models.Enums.OrderState.Cancelled));
    }
}
