using BusinessLayer.DTOs.Order.Create;
using BusinessLayer.DTOs.Order.View;

namespace BusinessLayer.Facades.Order;

public interface IOrderFacade
{
    Task<DetailedOrderViewDto> CreateOrderAsync(long userId);
    Task<IEnumerable<GeneralOrderViewDto>> FetchOrdersByUserIdAsync(long userId);
    Task<DetailedOrderViewDto> FindOrderByIdAsync(long id);
    Task<DetailedOrderViewDto> PayForOrderAsync(long id);
    Task<DetailedOrderViewDto> CancelOrderAsync(long id);
    Task<DetailedOrderViewDto> RefundOrderAsync(long id);
    Task DeleteOrderByIdAsync(long id);
    Task<DetailedOrderViewDto> CreateOrderItem(long orderId, CreateOrderItemDto createOrderItemDto);
    Task<DetailedOrderItemViewDto> FindOrderItemByIdAsync(long id);
    Task DeleteOrderItemByIdAsync(long id);
}
