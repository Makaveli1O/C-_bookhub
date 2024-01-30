namespace MVC.Models.WishList;

public class CreateWishListItemViewModel
{
    public long WishListId { get; set; }
    public long BookId { get; set; }
    public string? BookTitle { get; set; }
    public string? BookISBN {  get; set; }    
    public uint PreferencePriority { get; set; }
}
