namespace MVC.Models.WishList;

public class WishListViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
}
