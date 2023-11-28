using AutoMapper;
using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.WishList.Create;
using BusinessLayer.DTOs.WishList.View;
using BusinessLayer.Services;
using BusinessLayer.Services.Book;

namespace BusinessLayer.Facades.WishList;

public class WishListFacade : BaseFacade, IWishListFacade
{
    private readonly IGenericService<WishListEntity, long> _wishListService;
    private readonly IGenericService<WishListItemEntity, long> _wishListItemService;
    private readonly IBookService _bookService;

    public WishListFacade(IMapper mapper,
                          IGenericService<WishListEntity, long> wishListService,
                          IGenericService<WishListItemEntity, long> wishListItemService,
                          IBookService bookService)
        : base(mapper)
    {
        _wishListService = wishListService;
        _wishListItemService = wishListItemService;
        _bookService = bookService;
    }

    public async Task<GeneralWishListViewDto> CreateWishListAsync(CreateWishListDto createWishListDto)
    {
        var wishList = _mapper.Map<WishListEntity>(createWishListDto);

        var createdWishList = await _wishListService.CreateAsync(wishList);

        return _mapper.Map<GeneralWishListViewDto>(createdWishList);
    }

    public async Task<GeneralWishListItemViewDto> CreateWishListItemAsync(CreateWishListItemDto createWishListItemDto)
    {
        var wishListItem = _mapper.Map<WishListItemEntity>(createWishListItemDto);
        var book = _mapper.Map<GeneralBookViewDto>(await _bookService.FindByIdAsync(createWishListItemDto.BookId));
        
        var createdWishListItem = await _wishListItemService.CreateAsync(wishListItem);
        var wishListItemView = _mapper.Map<GeneralWishListItemViewDto>(createdWishListItem);
        wishListItemView.Book = book;

        return wishListItemView;
    }
    public async Task<GeneralWishListViewDto> UpdateWishListAsync(long id, string? wishListDescription)
    {
        var wishList = await _wishListService.FindByIdAsync(id);

        wishList.Description = wishListDescription;

        await _wishListService.UpdateAsync(wishList);

        return _mapper.Map<GeneralWishListViewDto>(wishList);
    }
    
    public async Task<GeneralWishListItemViewDto> UpdateWishListItemAsync(long id, uint preferencePriority)
    {
        var wishListItem = await _wishListItemService.FindByIdAsync(id);
        var book = _mapper.Map<GeneralBookViewDto>(await _bookService.FindByIdAsync(wishListItem.BookId));

        wishListItem.PreferencePriority = preferencePriority;
        var updatedWishListItemDto = _mapper.Map<GeneralWishListItemViewDto>(await _wishListItemService.UpdateAsync(wishListItem));
        updatedWishListItemDto.Book = book;

        return updatedWishListItemDto;
    }
    
    public async Task DeleteWishListAsync(long id)
    {
        var wishList = await _wishListService.FindByIdAsync(id);
        await _wishListService.DeleteAsync(wishList);
    }
   
    public async Task DeleteWishListItemAsync(long id)
    {
        var wishListItem = await _wishListItemService.FindByIdAsync(id);
        await _wishListItemService.DeleteAsync(wishListItem);
    }
    
    public async Task DeleteWishListItemsAsync(long wishListId)
    {
        var wishList = await _wishListService.FindByIdAsync(wishListId);

        if (wishList.WishListItems != null) 
        {
            var deleteTasks = wishList.WishListItems.Select(item => _wishListItemService.DeleteAsync(item));
            await Task.WhenAll(deleteTasks);
        }
    }

    public async Task<IEnumerable<GeneralWishListItemViewDto>> FetchAllItemsFromWishListAsync(long wishListId)
    {
        var wishList = await _wishListService.FindByIdAsync(wishListId);
        return _mapper.Map<List<GeneralWishListItemViewDto>>(wishList?.WishListItems);
    }
  
    public async Task<GeneralWishListItemViewDto> FetchSingleItemFromWishListAsync(long itemId)
    {
        return _mapper.Map<GeneralWishListItemViewDto>(await _wishListItemService.FindByIdAsync(itemId));
    }
    
    public async Task<GeneralWishListViewDto> FetchWishListAsync(long id)
    {
        return _mapper.Map<GeneralWishListViewDto>(await _wishListService.FindByIdAsync(id));
    }
}
