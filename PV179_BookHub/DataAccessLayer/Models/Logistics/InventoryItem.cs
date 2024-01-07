using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models.Publication;

namespace DataAccessLayer.Models.Logistics
{
    public class InventoryItem : BaseEntity
    {
        public long BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public virtual Book? Book { get; set; }
        public long BookStoreId { get; set; }
        [ForeignKey(nameof(BookStoreId))]
        public virtual BookStore? BookStore { get; set; }
        public uint InStock { get; set; }
        public DateTime LastRestock { get; set; }
    }
}
