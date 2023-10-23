using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class OrderItem : BaseEntity
{
    public long OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public virtual Order? Order { get; set; }
    //public long SomeId { get; set; }
    //public required virtual Some Some { get; set; }
    public double Price { get; set; }
    public uint Quantity { get; set; }
}
