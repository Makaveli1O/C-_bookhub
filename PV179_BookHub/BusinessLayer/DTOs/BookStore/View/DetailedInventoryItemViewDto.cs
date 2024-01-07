namespace BusinessLayer.DTOs.BookStore.View;

public class DetailedInventoryItemViewDto
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public long BookStoreId { get; set; }
    public uint InStock { get; set; }
    public DateTime LastRestock { get; set; }
}
