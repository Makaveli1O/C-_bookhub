using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
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
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorBookAssociation> AuthorBookAssociations { get; set; }

    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {
    }

    private static void AddRelationsForAuthor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithMany(book => book.Authors)
            .UsingEntity<AuthorBookAssociation>();

        modelBuilder.Entity<Author>()
            .HasMany(author => author.AuthorBookAssociations)
            .WithOne(assoc => assoc.Author)
            .HasForeignKey(assoc => assoc.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationsForPublisher(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publisher>()
            .HasMany(publisher => publisher.Books)
            .WithOne(book => book.Publisher)
            .HasForeignKey(book => book.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void AddRelationsForBook(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasMany(book => book.AuthorBookAssociations)
            .WithOne(assoc => assoc.Book)
            .HasForeignKey(assoc => assoc.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(book => book.Reviews)
            .WithOne(review => review.Book)
            .HasForeignKey(review => review.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(book => book.InventoryItems)
            .WithOne(inventoryItem => inventoryItem.Book)
            .HasForeignKey(inventoryItem => inventoryItem.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(book => book.WishListItems)
            .WithOne(wishListItem => wishListItem.Book)
            .HasForeignKey(wishListItem => wishListItem.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(book => book.OrderItems)
            .WithOne(orderItem => orderItem.Book)
            .HasForeignKey(orderItem => orderItem.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static void AddRelationsForUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.BookReviews)
            .WithOne(review => review.Reviewer)
            .HasForeignKey(review => review.ReviewerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.WishLists)
            .WithOne(wishList => wishList.Creator)
            .HasForeignKey(wishList => wishList.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Orders)
            .WithOne(order => order.User)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static void AddRelationsForBookStore(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookStore>()
            .HasOne(store => store.Address)
            .WithOne(address => address.BookStore)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookStore>()
            .HasOne(store => store.Manager)
            .WithOne(user => user.BookStore)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<BookStore>()
            .HasMany(store => store.InventoryItems)
            .WithOne(item => item.BookStore)
            .HasForeignKey(item => item.BookStoreId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookStore>()
            .HasMany(store => store.OrderItems)
            .WithOne(item => item.BookStore)
            .HasForeignKey(item => item.BookStoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationsForWishList(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WishList>()
            .HasMany(wishlist => wishlist.WishListItems)
            .WithOne(item => item.WishList)
            .HasForeignKey(item => item.WishListId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationsForOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(order => order.Items)
            .WithOne(item => item.Order)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }

        /* here added relationships */
        AddRelationsForAuthor(modelBuilder);
        AddRelationsForPublisher(modelBuilder);
        AddRelationsForBook(modelBuilder);
        AddRelationsForUser(modelBuilder);
        AddRelationsForBookStore(modelBuilder);
        AddRelationsForWishList(modelBuilder);
        AddRelationsForOrder(modelBuilder);

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
