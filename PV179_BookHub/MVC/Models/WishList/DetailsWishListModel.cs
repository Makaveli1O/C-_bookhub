using BusinessLayer.DTOs.WishList.View;

namespace MVC.Models.WishList;

public class DetailsWishListModel
{
    public long Id { get; set; }
    public string? OwnerName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public IEnumerable<GeneralWishListItemViewDto> Items { get; set; } = Enumerable.Empty<GeneralWishListItemViewDto>();
}
