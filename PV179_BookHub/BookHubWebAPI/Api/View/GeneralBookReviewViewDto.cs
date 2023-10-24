using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookHubWebAPI.Api.View
{
    public class GeneralBookReviewViewDto
    {
        public long BookId { get; set; }
        public long ReviewerId { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
    }
}
