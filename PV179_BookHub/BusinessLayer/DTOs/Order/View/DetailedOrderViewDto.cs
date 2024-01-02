namespace BusinessLayer.DTOs.Order.View;

public class DetailedOrderViewDto : GeneralOrderViewDto
{
    public IEnumerable<GeneralOrderItemViewDto> Items { get; set; }
}
