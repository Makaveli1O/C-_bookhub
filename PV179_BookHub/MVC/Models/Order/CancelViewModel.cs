using DataAccessLayer.Models.Enums;

namespace MVC.Models.Order;

public class CancelViewModel
{
    public long OrderId { get; set; }
    public OrderState OrderState { get; set; }
}