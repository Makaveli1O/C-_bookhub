using BusinessLayer.DTOs.Order.View;
using DataAccessLayer.Models.Enums;

namespace MVC.Models.Order;

public class OrderFormModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public double TotalPrice { get; set; }
    public OrderState State { get; set; }
    public IEnumerable<DetailedOrderItemViewDto> Items { get; set; }
}
