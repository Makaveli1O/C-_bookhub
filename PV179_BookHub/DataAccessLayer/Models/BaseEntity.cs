using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
