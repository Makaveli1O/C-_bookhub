namespace BusinessLayer.DTOs.Author.Create;

public class CreateAuthorDto
{
    public required string Name { get; set; }
    public string? Biography { get; set; }
}
