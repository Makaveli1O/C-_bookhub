using BusinessLayer.Exceptions;
using DataAccessLayer.Models.Enums;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Order;

public class OrderService : GenericService<OrderEntity, long>, IOrderService
{
    public OrderService(IUnitOfWork unitOfWork, IQuery<OrderEntity, long> query) : base(unitOfWork, query)
    {
    }

    public async Task<bool> CheckForActiveOrdersByUserIdAsync(long userId)
    {
        var orders = await Repository.GetAllAsync(x => (x.UserId == userId) && (x.State == OrderState.Created));
        return orders.Any();
    }

    public async Task<IEnumerable<OrderEntity>> FetchAllByUserIdAsync(long userId)
    {
        return await Repository.GetAllAsync(x => x.UserId == userId, x => x.Items);
    }

    public override async Task<OrderEntity> FindByIdAsync(long id)
    {
        var order = await Repository.GetByIdAsync(id, order => order.Items);
        if (order == null)
        {
            throw new NoSuchEntityException<long>(typeof(OrderEntity), id);
        }
        return order;
    }
}
