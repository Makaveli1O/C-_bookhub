using BusinessLayer.DTOs.Book.View;
using DataAccessLayer.Models.Enums;

namespace MVC.Models.User;

public class UserDetailViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public UserRole Role { get; set; }

    public IEnumerable<GeneralBookViewDto>? recommendations { get; set; }

}
