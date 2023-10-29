using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class BookHubDbContext : DbContext
{
    public DbSet<BookStore> BookStores { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<WishList> WishList { get; set; }
    public DbSet<WishListItem> WishListItem { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BookReview> BookReviews { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }

        /* here added relationships */
        modelBuilder.Entity<InventoryItem>()
            .HasOne(item => item.BookStore)
            .WithMany(bookStore => bookStore.InventoryItems)
            .HasForeignKey(item => item.BookStoreId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishListItem>()
            .HasOne(item => item.WishList)
            .WithMany(wishList => wishList.WishListItems)
            .HasForeignKey(item => item.WishListId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookReview>()
            .HasOne(review => review.Book)
            .WithMany(book => book.Reviews)
            .HasForeignKey(review => review.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
