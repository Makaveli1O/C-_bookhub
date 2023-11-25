using BusinessLayer.DTOs.User.Create;
using BusinessLayer.DTOs.User.View;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
