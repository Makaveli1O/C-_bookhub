namespace MVC.Models.InventoryItem;

public class InventoryItemCreateViewModel
{
    public long BookId { get; set; }
    public long BookStoreId { get; set; }
    public uint InStock { get; set; }
    public DateTime LastRestock { get; set; }
}
