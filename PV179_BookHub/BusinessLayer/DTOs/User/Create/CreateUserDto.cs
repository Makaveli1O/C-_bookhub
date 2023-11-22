using DataAccessLayer.Models.Enums;

namespace BusinessLayer.DTOs.User.Create
{
    public class CreateUserDto
    {
        //TODO user is not supposed to send PW hash neither salt
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
