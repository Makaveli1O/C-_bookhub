namespace BusinessLayer.DTOs.Publisher.View;

public class GeneralPublisherViewDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public ushort YearFounded { get; set; }
}
