using MVC.Models.Base;

namespace MVC.Models.Author;

public class AuthorSearchModel : BaseSearchModel<AuthorSortParameters>
{
    public string? CONTAINS_Name { get; set; }
    public string? CONTAINS_Biography { get; set; }
}
