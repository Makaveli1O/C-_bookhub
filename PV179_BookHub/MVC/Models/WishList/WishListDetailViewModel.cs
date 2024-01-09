using BusinessLayer.DTOs.WishList.View;

namespace MVC.Models.WishList;

public class WishListDetailViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public IEnumerable<GeneralWishListItemViewDto>? Items { get; set; }
}
