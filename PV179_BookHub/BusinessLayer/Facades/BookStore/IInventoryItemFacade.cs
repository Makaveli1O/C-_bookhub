using BusinessLayer.DTOs.Book.View;
using BusinessLayer.DTOs.BookStore.Create;
using BusinessLayer.DTOs.BookStore.View;

namespace BusinessLayer.Facades.BookStore;

public interface IInventoryItemFacade
{
    Task<DetailedInventoryItemViewDto> CreateInventoryItem(CreateInventoryItemDto createInventoryItemDto);
    Task<DetailedInventoryItemViewDto> UpdateInventoryItem(long id, CreateInventoryItemDto updateInventoryItemDto);
    Task<IEnumerable<DetailedInventoryItemViewDto>> GetAllInventoryItems();
    Task<IEnumerable<DetailedBookViewDto>> GetUniqueInventoryItems();
    Task<IEnumerable<DetailedInventoryItemViewDto>> GetAllInventoryItemsByUserId(long id);
    Task<DetailedInventoryItemViewDto> GetInventoryItem(long id);
    Task DeleteInventoryItem(long id);
}
