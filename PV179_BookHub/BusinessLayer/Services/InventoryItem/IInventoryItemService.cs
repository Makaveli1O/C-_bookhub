

namespace BusinessLayer.Services.InventoryItem;

public interface IInventoryItemService : IGenericService<InventoryItemEntity, long>
{
    Task ChangeStockAsync(long bookId, long bookStoreId, uint quantity, StockDirection stockDirection, bool save = true);
}
