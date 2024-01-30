using BusinessLayer.DTOs.BaseFilter;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.Filter;
using BusinessLayer.DTOs.WishList.View;

namespace BusinessLayer.Facades.WishList;

public interface IWishListFacade
{
    Task<GeneralWishListViewDto> CreateWishListAsync(CreateWishListDto createWishListDto);
    Task<GeneralWishListItemViewDto> CreateWishListItemAsync(CreateWishListItemDto createWishListItemDto);
    Task<GeneralWishListViewDto> UpdateWishListAsync(long id, string? wishListDescription);
    Task<GeneralWishListItemViewDto> UpdateWishListItemAsync(long id, uint preferencePriority);
    Task DeleteWishListAsync(long id);
    Task DeleteWishListItemAsync(long id);
    Task DeleteWishListItemsAsync(long wishListId);
    Task<IEnumerable<GeneralWishListItemViewDto>> FetchAllItemsFromWishListAsync(long wishListId);
    Task<GeneralWishListItemViewDto> FetchSingleItemFromWishListAsync(long itemId);
    Task<GeneralWishListViewDto> FetchWishListAsync(long id);
    Task<IEnumerable<GeneralWishListViewDto>> FetchAllByUserIdAsync(long userId);
    Task<FilterResultDto<GeneralWishListViewDto>> FetchFilteredWishListsAsync(WishListFilterDto wishListFilterDto);
}
