using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Publication;

namespace Infrastructure.NaiveQuery.Filters.EnitityFilters;

public class BookFilter : FilterBase<Book>
{
    public BookFilter() : base()
    {
    }

    protected override void SetUpSpecialLambdaExpressions()
    {
        _lambdaDictionary.Add("Title", source => source.Title.Contains(Title));
        _lambdaDictionary.Add("Author", source => source.Authors.Any(author => author.Name.Contains(Author)));
        _lambdaDictionary.Add("Publisher", source => source.Publisher.Name.Contains(Publisher));
        _lambdaDictionary.Add("Description", source => source.Description.Contains(Description));
    }

    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? Description { get; set; }
    public BookGenre? BookGenre { get; set; }
    public double? LEQ_Price { get; set; }
    public double? GEQ_Price { get; set; }
}
