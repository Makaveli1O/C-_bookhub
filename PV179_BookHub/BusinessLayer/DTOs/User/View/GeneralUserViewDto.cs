using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.User.View;

public class GeneralUserViewDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public UserRole Role { get; set; }
}
