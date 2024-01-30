using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.WishListItem;

public class WishListItemService : GenericService<WishListItemEntity, long>, IWishListItemService
{
    public WishListItemService(IUnitOfWork unitOfWork, IQuery<WishListItemEntity, long> query) : base(unitOfWork, query)
    {
    }

    public async Task<IEnumerable<WishListItemEntity>> FetchItemsByWishListIdAsync(long wishListId)
    {
        return await Repository
            .GetAllAsync(
                item => item.WishListId == wishListId,
                item => item.Book
                );
    }
}
