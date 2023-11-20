using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Publication;

namespace Infrastructure.NaiveQuery.Filters.EntityFilters;

public class BookFilter : FilterBase<Book>
{
    public BookFilter(IDictionary<string, string> filters) : base()
    {
        foreach (var property in GetType().GetProperties())
        {
            var propertyName = property.Name.ToLower();
            if (!filters.ContainsKey(propertyName))
            {
                continue;
            }

            if (property.PropertyType == typeof(double?))
            {
                property.SetValue(this, Convert.ToDouble(filters[propertyName]));
            }
            else if (property.PropertyType == typeof(BookGenre?))
            {
                property.SetValue(this, Enum.Parse<BookGenre>(filters[propertyName]));
            }
            else
            {
                property.SetValue(this, filters[propertyName]);
            }
        }
    }

    protected override void SetUpSpecialLambdaExpressions()
    {
        _lambdaDictionary.Add("Title", source => source.Title.Contains(Title));
        _lambdaDictionary.Add("Author", source => source.Authors.Any(author => author.Name.Contains(Author)));
        _lambdaDictionary.Add("Publisher", source => source.Publisher.Name.Contains(Publisher));
    }

    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public BookGenre? BookGenre { get; set; }
    public double? Price { get; set; }
}
