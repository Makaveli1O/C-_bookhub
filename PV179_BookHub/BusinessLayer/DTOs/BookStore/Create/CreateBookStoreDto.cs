using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.BookStore.Create
{
    public class CreateBookStoreDto
    {
        public long AddressId { get; set; }
        public long ManagerId { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
}
