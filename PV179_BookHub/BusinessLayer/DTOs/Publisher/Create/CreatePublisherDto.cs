
namespace BusinessLayer.DTOs.Publisher.Create;

public class CreatePublisherDto
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public ushort? YearFounded { get; set; }
}
