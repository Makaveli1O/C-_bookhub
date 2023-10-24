
namespace BookHubWebAPI.Api.Create;

public class CreateOrderItemDto
{
    public long BookId { get; set; }
    //public long BookStoreId { get; set; }
    public double Price { get; set; }
    public uint Quantity { get; set; }
}
