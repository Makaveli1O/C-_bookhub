

namespace BusinessLayer.Services.Order;

public interface IOrderService : IGenericService<OrderEntity, long>
{
    Task<IEnumerable<OrderEntity>> FetchAllByUserIdAsync(long userId);
    Task<bool> CheckForActiveOrdersByUserIdAsync(long userId);
}
