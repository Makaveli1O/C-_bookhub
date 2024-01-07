using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Purchasing;

namespace DataAccessLayer.Models.Logistics
{
    public class BookStore : BaseEntity
    {

        public long AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual Address? Address { get; set; }
        public long ManagerId { get; set; }
        [ForeignKey(nameof(ManagerId))]
        public virtual User? Manager { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        [MaxLength(20)]
        public required string PhoneNumber { get; set; }
        [MaxLength(50)]
        public required string Email { get; set; }
        public virtual IEnumerable<InventoryItem>? InventoryItems { get; set; }
        public virtual IEnumerable<OrderItem>? OrderItems { get; set; }
    }
}
