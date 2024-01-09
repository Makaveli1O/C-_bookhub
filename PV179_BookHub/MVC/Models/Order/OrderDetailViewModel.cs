using BusinessLayer.DTOs.Order.View;
using DataAccessLayer.Models.Enums;

namespace MVC.Models.Order;

public class OrderDetailViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public double TotalPrice { get; set; }
    public OrderState State { get; set; }
    public required IList<GeneralOrderItemViewDto> Items { get; set; }
}
