using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;

namespace TestUtilities.MockedData;

public static class TestDataInitializer
{
    public static List<Author> GetTestAuthors()
    {
        return new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Jamie Oliver",
                Biography = "I am cooking"
            },
            new Author()
            {
                Id = 2,
                Name = "Heisenberg",
                Biography = "Let him Cook"
            },
            new Author()
            {
                Id = 3,
                Name = "John Wick",
                Biography = "I am not cooking anymore, retired"
            }
        };
    }

    public static List<Publisher> GetTestPublishers()
    {
        return new List<Publisher>()
        {
            new Publisher()
            {
                Id = 1,
                Name = "Horizon Publications",
                City = "Tokyo",
                Country = "Japan",
                YearFounded = 2013
            },
            new Publisher()
            {
                Id = 2,
                Name = "Enigma Press",
                City = "Berlin",
                Country = "Germany",
                YearFounded = 1942
            },
            new Publisher()
            {
                Id = 3,
                Name = "Addison-Wesley Professional",
                Country = "Ireland",
                YearFounded = 2018
            },
        };
    }

    public static List<Book> GetTestBooks()
    {
        return new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Eternal Waters",
                ISBN = "978-1234567890",
                PublisherId = 3,
                BookGenre = BookGenre.Science,
                Description = "Something interesting",
                Price = 24.99
            },
            new Book
            {
                Id = 2,
                Title = "Secrets of the Last Scroll",
                ISBN = "111-2850195739",
                PublisherId = 1,
                BookGenre = BookGenre.Science,
                Description = "Its cool",
                Price = 18.95
            },
            new Book
            {
                Id = 3,
                Title = "New c-like language C--",
                ISBN = "978-0136816485",
                PublisherId = 2,
                BookGenre = BookGenre.Programming,
                Description = "Its the best",
                Price = 29.99
            },
        };
    }

    public static List<AuthorBookAssociation> GetTestAuthorBookAssociations()
    {
        return new List<AuthorBookAssociation>
        {
            new AuthorBookAssociation()
            {
                Id = 1,
                AuthorId = 1,
                BookId = 1
            },
            new AuthorBookAssociation()
            {
                Id = 2,
                AuthorId = 2,
                BookId = 1
            },
            new AuthorBookAssociation()
            {
                Id = 3,
                AuthorId = 3,
                BookId = 1
            },
            new AuthorBookAssociation()
            {
                Id = 4,
                AuthorId = 1,
                BookId = 2
            },
            new AuthorBookAssociation()
            {
                Id = 5,
                AuthorId = 3,
                BookId = 3
            }
        };
    }

    public static List<User> GetTestUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "JackTheRipper",
                PasswordHash = "954b39223c4cfd375e5b41ef79cdbe5cacaf9176",
                Salt = "8y4z6E",
                Role = UserRole.Admin
            },
            new User
            {
                Id = 2,
                UserName = "olivia.johnson@gmail.com",
                PasswordHash = "b5d66e00c0673d769f25c9919756341d34162cef",
                Salt = "3M9r1N",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 3,
                UserName = "liamthereaded",
                PasswordHash = "fd3a0c6a60faa4f9e487f04e153f17919219bcbc",
                Salt = "ab7x9D",
                Role = UserRole.User
            }
        };
    }

    public static List<BookReview> GetTestBookReviews()
    {
        return new List<BookReview>()
        {
            new BookReview
            {
                Id = 1,
                BookId = 1,
                ReviewerId = 3,
                Description = "Very good!",
                Rating = Rating.Excellent
            },
            new BookReview
            {
                Id = 2,
                BookId = 2,
                ReviewerId = 3,
                Description = "My second review",
                Rating = Rating.Excellent
            },
            new BookReview
            {
                Id = 3,
                BookId = 3,
                ReviewerId = 3,
                Description = "Just good, nothing special",
                Rating = Rating.Good
            }
        };
    }

    public static List<Address> GetTestAddresses()
    {
        return new List<Address>()
        {
            new Address
            {
                Id = 1,
                Street = "Palackého třída",
                StreetNumber = "191/241",
                City = "Brno",
                State = "Czech Republic",
                PostalCode = "602 00"
            },
            new Address
            {
                Id = 2,
                Street = "456 Elm St",
                StreetNumber = "Unit 7",
                City = "Sampletown",
                State = "NY",
                PostalCode = "67890"
            },
        };
    }

    public static List<BookStore> GetTestBookStores()
    {
        return new List<BookStore>()
        {
            new BookStore
            {
                Id = 1,
                AddressId = 1,
                ManagerId = 2,
                Name = "Brno Michelle Bookstore",
                PhoneNumber = "+421 918 365 172",
                Email = "bestBookstore@gmail.com",
            },
            new BookStore
            {
                Id = 2,
                AddressId = 3,
                ManagerId = 2,
                Name = "City Reads",
                PhoneNumber = "+123 456 7890",
                Email = "cityreads@example.com",
            },
        };
    }

    public static List<InventoryItem> GetTestInventoryItems()
    {
        return new List<InventoryItem>
        {
            new InventoryItem
            {
                Id = 1,
                BookId = 1,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 2,
                BookId = 2,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 3,
                BookId = 3,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 4,
                BookId = 1,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 5,
                BookId = 2,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 6,
                BookId = 3,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            }
        };
    }

    public static List<WishList> GetTestWishLists()
    {
        return new List<WishList>
        {
            new WishList
            {
                Id = 1,
                UserId = 3,
                Description = "First wishlist"
            },
            new WishList
            {
                Id = 2,
                UserId = 3,
                Description = "Second wishlist"
            },
        };
    }

    public static List<WishListItem> GetTestWishListItem()
    {
        return new List<WishListItem>()
        {
            new WishListItem
            {
                Id = 1,
                WishListId = 1,
                BookId = 1,
                PreferencePriority = 1,
            },
            new WishListItem
            {
                Id = 2,
                WishListId = 1,
                BookId = 3,
                PreferencePriority = 2,
            },
            new WishListItem
            {
                Id = 3,
                WishListId = 2,
                BookId = 2,
                PreferencePriority = 1,
            },
        };
    }

    public static List<Order> GetTestOrderList()
    {
        return new List<Order>()
        {
            new Order
            {
                Id = 1,
                UserId = 3,
                State = OrderState.Cancelled,
                TotalPrice = 0
            },
            new Order
            {
                Id = 2,
                UserId = 3,
                State = OrderState.Created,
                TotalPrice = 18.80
            },
        };
    }

    public static List<OrderItem> GetTestOrderItems() 
    {
        return new List<OrderItem>()
        {
            new OrderItem
            {
                Id = 1,
                BookId = 1,
                BookStoreId = 1,
                OrderId = 1,
                Price = 6.80,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 2,
                BookId = 3,
                BookStoreId = 2,
                OrderId = 2,
                Price = 6.80,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 3,
                BookId = 1,
                BookStoreId = 2,
                OrderId = 2,
                Price = 6.00,
                Quantity = 2
            }
        };
    }
}
