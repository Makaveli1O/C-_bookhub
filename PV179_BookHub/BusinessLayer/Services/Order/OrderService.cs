using BusinessLayer.Exceptions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.Order;

public class OrderService : GenericService<DataAccessLayer.Models.Purchasing.Order, long>, IOrderService
{
    public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<IEnumerable<DataAccessLayer.Models.Purchasing.Order>> FetchAllByUserIdAsync(long userId)
    {
        return await Repository.GetAllAsync(x => x.UserId == userId);
    }

    public override async Task<DataAccessLayer.Models.Purchasing.Order> FindByIdAsync(long id)
    {
        var order = await Repository.GetByIdAsync(id, order => order.Items);
        if (order == null)
        {
            throw new NoSuchEntityException<long>(typeof(DataAccessLayer.Models.Purchasing.Order), id);
        }
        return order;
    }
}
