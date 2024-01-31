using BusinessLayer.DTOs.BaseFilter;

namespace BusinessLayer.DTOs.WishList.Filter;

public class WishListFilterDto : FilterDto
{
    public long? UserId { get; set; }
    public string? CONTAINS_Description { get; set; }
    public DateTime? GE_CreatedAt { get; set; }
    public DateTime? LE_CreatedAt { get; set; }
}
