namespace BusinessLayer.DTOs.Order.View;

public class DetailedOrderViewDto : GeneralOrderViewDto
{
    public IEnumerable<DetailedOrderItemViewDto> Items { get; set; }
}
