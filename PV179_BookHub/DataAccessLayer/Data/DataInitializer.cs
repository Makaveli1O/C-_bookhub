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
        var orders = PrepairOrderModels();
        var orderItems = PrepairOrderItemModels();
        var users = PrepairUserModels();
        var bookReviews = PrepairBookReviews();
        var addresses = PrepairAddressModels();

        modelBuilder.Entity<User>()
            .HasData(users);
        modelBuilder.Entity<Book>()
            .HasData(books);
        modelBuilder.Entity<BookReview>()
            .HasData(bookReviews);
        modelBuilder.Entity<WishList>()
            .HasData(wishLists);
        modelBuilder.Entity<WishListItem>()
            .HasData(bowishListItems);
        modelBuilder.Entity<Order>()
            .HasData(orders);
        modelBuilder.Entity<OrderItem>()
            .HasData(orderItems);
        modelBuilder.Entity<Address>()
            .HasData(addresses);

    }

    private static List<BookReview> PrepairBookReviews()
    {
        return new List<BookReview>
        {
        };
    }

    private static List<User> PrepairUserModels()
    {
        return new List<User>
        {
        };
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

    private static List<Order> PrepairOrderModels()
    {
        return new List<Order>()
        {
        };
    }

    private static List<OrderItem> PrepairOrderItemModels()
    {
        return new List<OrderItem>()
        {
        };
    }

    private static List<Address> PrepairAddressModels()
    {
        return new List<Address>()
        {
        };
    }
}
