using DataAccessLayer.Models;

namespace BookHubWebAPI.Api.View;

public class GeneralWishListViewDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Descripton { get; set; } = string.Empty;
    public virtual IEnumerable<WishListItem>? WishListItems { get; set; }
}
