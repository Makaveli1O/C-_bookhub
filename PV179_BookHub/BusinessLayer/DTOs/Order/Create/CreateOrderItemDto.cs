namespace BusinessLayer.DTOs.Order.Create;

public class CreateOrderItemDto
{
    public long BookId { get; set; }
    public long BookStoreId { get; set; }
    public uint Quantity { get; set; }
}
