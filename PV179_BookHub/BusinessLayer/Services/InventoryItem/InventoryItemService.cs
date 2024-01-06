using BusinessLayer.Exceptions;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Services.InventoryItem;

public class InventoryItemService : GenericService<InventoryItemEntity, long>, IInventoryItemService
{
    public InventoryItemService(IUnitOfWork unitOfWork, IQuery<InventoryItemEntity, long> query) : base(unitOfWork, query)
    {
    }

    public async Task ChangeStockAsync(long bookId, long bookStoreId, uint quantity, StockDirection stockDirection, bool save = true)
    {
        var inventoryItem = await Repository
            .GetSingleAsync(x => 
            (x.BookId == bookId) && (x.BookStoreId == bookStoreId));

        if (inventoryItem == null)
        {
            throw new NoSuchEntityException<long>(typeof(InventoryItemEntity), 0);
        }

        if (stockDirection == StockDirection.StockReduction)
        {
            if (quantity > inventoryItem.InStock)
            {
                throw new StockErrorException(inventoryItem.InStock, quantity);
            }
            inventoryItem.InStock -= quantity;
        }
        else if (stockDirection == StockDirection.StockAddition)
        {
            inventoryItem.InStock += quantity;
        }

        inventoryItem.LastRestock = DateTime.UtcNow;

        await UpdateAsync(inventoryItem);
    }
}
