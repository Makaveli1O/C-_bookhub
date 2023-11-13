using BookHubWebAPI.Api.Book.View;

namespace BookHubWebAPI.Api.Order.View;

public class DetailedOrderItemViewDto
{
    public long OrderId { get; set; }
    public double Price { get; set; }
    public uint Quantity { get; set; }
    public GeneralBookViewDto? Book { get; set; }
}
