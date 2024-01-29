namespace MVC.Models.Base;

public class GenericFilteredModel<TSearchModel, TFilteredEntity>
{
    public TSearchModel? SearchModel { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public IEnumerable<TFilteredEntity> Items { get; set; } = Enumerable.Empty<TFilteredEntity>();
}
