

namespace BusinessLayer.Services.Order;

public interface IOrderService : IGenericService<DataAccessLayer.Models.Purchasing.Order, long>
{
    Task<IEnumerable<DataAccessLayer.Models.Purchasing.Order>> FetchAllByUserIdAsync(long userId);
}
