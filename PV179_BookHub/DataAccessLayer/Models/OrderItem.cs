using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class OrderItem : BaseEntity
{
    public long OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public virtual Order? Order { get; set; }
    public long BookId { get; set; }
    [Required]
    public virtual Book? Book { get; set; }
    [Required]
    public long BookStoreId { get; set; }
    [ForeignKey(nameof(BookStoreId))]
    public virtual BookStore? BookStore { get; set; }
    public double Price { get; set; }
    public uint Quantity { get; set; }
}
