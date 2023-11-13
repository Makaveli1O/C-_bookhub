namespace BookHubWebAPI.Api.BookStore.View
{
    public class DetailedInventoryItemViewDto
    {
        public long InventoryItemId { get; set; }
        public long BookId { get; set; }
        public long BookStoreId { get; set; }
        public uint InStock { get; set; }
        public DateTime LastRestock { get; set; }
    }
}
