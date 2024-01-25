using BusinessLayer.DTOs.BaseFilter;

namespace BusinessLayer.DTOs.Publisher.Filter;

public class PublisherFilterDto : FilterDto
{
    public string? CONTAINS_Name { get; set; }
    public string? CONTAINS_City { get; set; }
    public string? CONTAINS_Country { get; set; }
    public ushort? GE_YearFounded { get; set; }
    public ushort? LE_YearFounded { get; set; }
}
