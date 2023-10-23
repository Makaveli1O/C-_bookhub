using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var books = PrepairBookModels();
        var wishLists = PrepairWishListModels();
        var bowishListItems = PrepairWishListItemModels();

        modelBuilder.Entity<Book>()
            .HasData(books);
        modelBuilder.Entity<WishList>()
            .HasData(wishLists);
        modelBuilder.Entity<WishListItem>()
            .HasData(bowishListItems);
    }

    private static List<Book> PrepairBookModels()
    {
        return new List<Book>()
        {
        };
    }

    private static List<WishList> PrepairWishListModels() 
    {
        return new List<WishList>()
        { 
        };
    }

    private static List<WishListItem> PrepairWishListItemModels()
    {
        return new List<WishListItem>()
        {
        };
    }
}
