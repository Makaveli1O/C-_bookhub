using DataAccessLayer.Models.Enums;

namespace BookHubWebAPI.Api.Order.View;

public class GeneralOrderViewDto
{
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public double TotalPrice { get; set; }
    public OrderState State { get; set; }
}
