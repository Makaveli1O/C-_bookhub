using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.WishList;

public class WishListService : GenericService<WishListEntity, long>, IWishListService
{
    public WishListService(IUnitOfWork unitOfWork, IQuery<WishListEntity, long> query) : base(unitOfWork, query)
    {
    }

    public async Task<IEnumerable<WishListEntity>> FetchAllByUserIdAsync(long userId)
    {
        return await Repository.GetAllAsync(x => x.UserId == userId, x => x.WishListItems);
    }
}
