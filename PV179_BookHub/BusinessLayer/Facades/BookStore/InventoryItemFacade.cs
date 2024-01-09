using AutoMapper;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;
using BusinessLayer.Services;
using BusinessLayer.Services.BookStore;
using BusinessLayer.Services.InventoryItem;

namespace BusinessLayer.Facades.BookStore;

public class InventoryItemFacade : BaseFacade, IInventoryItemFacade
{
    private readonly IInventoryItemService _inventoryItemService;
    private readonly IGenericService<BookEntity, long> _bookService;
    private readonly IBookStoreService _bookStoreService;

    public InventoryItemFacade(
        IMapper mapper,
        IInventoryItemService inventoryItemService,
        IBookStoreService bookStoreService,
        IGenericService<BookEntity, long> bookService)
        : base(mapper, null, null)
    {
        _inventoryItemService = inventoryItemService;
        _bookService = bookService;
        _bookStoreService = bookStoreService;
    }

    public async Task<DetailedInventoryItemViewDto> CreateInventoryItem(CreateInventoryItemDto createInventoryItemDto)
    {
        return _mapper.Map<DetailedInventoryItemViewDto>(
            await _inventoryItemService.CreateAsync(
                _mapper.Map<InventoryItemEntity>(createInventoryItemDto)));
    }

    public async Task DeleteInventoryItem(long id)
    {
        await _inventoryItemService.DeleteAsync(await _inventoryItemService.FindByIdAsync(id));
    }

    public async Task<IEnumerable<DetailedInventoryItemViewDto>> GetAllInventoryItems()
    {
        return _mapper.Map<List<DetailedInventoryItemViewDto>>(await _inventoryItemService.FetchAllAsync());
    }

    public async Task<DetailedInventoryItemViewDto> GetInventoryItem(long id)
    {
        return _mapper.Map<DetailedInventoryItemViewDto>(await _inventoryItemService.FindByIdAsync(id));
    }

    public async Task<DetailedInventoryItemViewDto> UpdateInventoryItem(long id, CreateInventoryItemDto updateInventoryItemDto)
    {
        var inventoryItem = await _inventoryItemService.FindByIdAsync(id);

        // will throw not found exception if entities do not exist
        await _bookService.FindByIdAsync(updateInventoryItemDto.BookId);
        await _bookStoreService.FindByIdAsync(updateInventoryItemDto.BookStoreId);

        inventoryItem.InStock = updateInventoryItemDto.InStock;
        inventoryItem.LastRestock = updateInventoryItemDto.LastRestock;
        inventoryItem.BookStoreId = updateInventoryItemDto.BookStoreId;
        inventoryItem.BookId = updateInventoryItemDto.BookId;
        await _inventoryItemService.UpdateAsync(inventoryItem);
       
        return _mapper.Map<DetailedInventoryItemViewDto>(inventoryItem);
    }
    public async Task<IEnumerable<DetailedInventoryItemViewDto>> GetAllInventoryItemsByUserId(long id)
    {
        var bookStore = await _bookStoreService.GetBookStoreByUserIdAsync(id);

        return _mapper.Map<IEnumerable<DetailedInventoryItemViewDto>>(await _inventoryItemService.GetInventoryItemsByBookStoreIdAsync(bookStore.Id));
    }
}
