namespace BusinessLayer.Services.WishList;

public interface IWishListService : IGenericService<WishListEntity, long>
{
    Task<IEnumerable<WishListEntity>> FetchAllByUserIdAsync(long userId);
}
