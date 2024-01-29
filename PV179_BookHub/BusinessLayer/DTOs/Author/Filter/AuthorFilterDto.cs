using BusinessLayer.DTOs.BaseFilter;

namespace BusinessLayer.DTOs.Author.Filter;

public class AuthorFilterDto : FilterDto
{
    public string? CONTAINS_Name { get; set; }
    public string? CONTAINS_Biography { get; set; }
}
