using DataAccessLayer.Models;
using DataAccessLayer.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookHubWebAPI.Api.View
{
    public class GeneralUserViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public UserRole Role { get; set; }
    }
}
