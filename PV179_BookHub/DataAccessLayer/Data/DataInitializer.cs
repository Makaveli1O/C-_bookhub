using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public static class DataInitializer
    {
        public static vo id Seed(this ModelBuilder modelBuilder)
        {
            var books = PrepairBookModels();

            modelBuilder.Entity<Book>()
                .HasData(books);
        }

        private static List<Book> PrepairBookModels()
        {
            return new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Description = "BlaBla",
                    Title = "Title",
                },
            };
        }
    }
}
