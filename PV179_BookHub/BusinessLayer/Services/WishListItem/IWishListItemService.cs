namespace BusinessLayer.Services.WishListItem;

public interface IWishListItemService : IGenericService<WishListItemEntity, long>
{
    Task<IEnumerable<WishListItemEntity>> FetchItemsByWishListIdAsync(long wishListId);
}
