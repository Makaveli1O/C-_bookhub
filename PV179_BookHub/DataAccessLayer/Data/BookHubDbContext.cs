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

    private static void AddRelationshipsForInventoryItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItem>()
            .HasOne(item => item.BookStore)
            .WithMany(bookStore => bookStore.InventoryItems)
            .HasForeignKey(item => item.BookStoreId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<InventoryItem>()
            .HasOne(item => item.Book)
            .WithMany(book => book.InventoryItems)
            .HasForeignKey(item => item.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationshipsForOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(order => order.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(order => order.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.BookStore)
            .WithMany(store => store.OrderItems)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Book)
            .WithMany(book => book.OrderItems)
            .OnDelete(DeleteBehavior.NoAction);
    }

    private static void AddRelationshipsForWishList(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WishList>()
            .HasOne(wishlist => wishlist.Creator)
            .WithMany(creator => creator.WishLists)
            .HasForeignKey(wishlist => wishlist.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishListItem>()
            .HasOne(item => item.WishList)
            .WithMany(wishList => wishList.WishListItems)
            .HasForeignKey(item => item.WishListId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WishListItem>()
            .HasOne(item => item.Book)
            .WithMany(book => book.WishListItems)
            .HasForeignKey(item => item.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationshipsForBookStore(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookStore>()
            .HasOne(store => store.Address)
            .WithOne(address => address.BookStore)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookStore>()
            .HasOne(store => store.Manager)
            .WithOne(user => user.BookStore)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationshipsForBookReview(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookReview>()
            .HasOne(review => review.Book)
            .WithMany(book => book.Reviews)
            .HasForeignKey(review => review.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BookReview>()
            .HasOne(review => review.Reviewer)
            .WithMany(user => user.BookReviews)
            .HasForeignKey(review => review.ReviewerId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void AddRelationshipsForBook(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(book => book.Publisher)
            .WithMany(publisher => publisher.Books)
            .HasForeignKey(book => book.PublisherId);

        modelBuilder.Entity<Book>()
            .HasMany(book => book.Authors)
            .WithMany(author => author.Books)
            .UsingEntity<AuthorBookAssociation>();
    }

    private static void AddRelationshipsForAuthorBookAssociations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBookAssociation>()
            .HasOne(assoc => assoc.Book)
            .WithMany(book => book.AuthorBookAssociations)
            .HasForeignKey(assoc => assoc.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AuthorBookAssociation>()
            .HasOne(assoc => assoc.Author)
            .WithMany(author => author.AuthorBookAssociations)
            .HasForeignKey(assoc => assoc.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }

        /* here added relationships */
        AddRelationshipsForAuthorBookAssociations(modelBuilder);
        AddRelationshipsForBook(modelBuilder);
        AddRelationshipsForInventoryItem(modelBuilder);
        AddRelationshipsForOrder(modelBuilder);
        AddRelationshipsForWishList(modelBuilder);
        AddRelationshipsForBookStore(modelBuilder);
        AddRelationshipsForBookReview(modelBuilder);

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
