using MVC.Models.Base;

namespace MVC.Models.Publisher;

public class PublisherSearchModel : BaseSearchModel<PublisherSortParameters>
{
    public string? CONTAINS_Name { get; set; }
    public string? CONTAINS_City { get; set; }
    public string? CONTAINS_Country {  get; set; }
    public ushort? GE_YearFounded {  get; set; }
    public ushort? LE_YearFounded { get; set; }
}
