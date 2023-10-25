using DataAccessLayer.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models 
{
    public class BookReview : BaseEntity
    {
        public long BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public virtual required Book Book { get; set; }
        public long ReviewerId { get; set; }
        [ForeignKey(nameof(ReviewerId))]
        public virtual required User Reviewer { get; set; }
        [MaxLength(5000)]
        public string? Description { get; set; }
        public Rating Rating { get; set; }
    }
}
