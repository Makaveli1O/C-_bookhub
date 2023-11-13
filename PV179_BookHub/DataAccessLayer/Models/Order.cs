using DataAccessLayer.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Order : BaseEntity
{
    public long UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public double TotalPrice { get; set; }
    public OrderState State { get; set; }
    public virtual IEnumerable<OrderItem>? Items { get; set; }
}
