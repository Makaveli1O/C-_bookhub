using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Publication;

namespace DataAccessLayer.Models.Purchasing;

public class OrderItem : BaseEntity
{
    public long OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public virtual Order? Order { get; set; }
    [Required]
    public long BookId { get; set; }
    public virtual Book? Book { get; set; }
    [Required]
    public long BookStoreId { get; set; }
    [ForeignKey(nameof(BookStoreId))]
    public virtual BookStore? BookStore { get; set; }
    public double Price { get; set; }
    public uint Quantity { get; set; }
}
