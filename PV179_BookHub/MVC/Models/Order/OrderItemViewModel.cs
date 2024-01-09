using BusinessLayer.DTOs.Book.View;

namespace MVC.Models.Order;

public class OrderItemViewModel
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public double Price { get; set; }
    public uint Quantity { get; set; }
    public GeneralBookViewDto? Book { get; set; }
}
