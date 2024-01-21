using DataAccessLayer.Models.Account;
using DataAccessLayer.Models.Enums;
using DataAccessLayer.Models.Logistics;
using DataAccessLayer.Models.Preferences;
using DataAccessLayer.Models.Publication;
using DataAccessLayer.Models.Purchasing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataAccessLayer.Data;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var authors = PrepareAuthorModels();
        var publishers = PreparePublisherModels();
        var books = PrepareBookModels();
        var authorBooksAssociations = PrepareAuthorBooksAssociationsModels();

        var users = PrepareUserModels();
        var bookReviews = PrepareBookReviews();

        var addresses = PrepareAddressModels();
        var bookStores = PrepareBookStoreModels();
        var inventoryItems = PrepareInventoryItemModels();

        var wishLists = PrepareWishListModels();
        var wishListItems = PrepareWishListItemModels();

        var orders = PrepareOrderModels();
        var orderItems = PrepareOrderItemModels();


        modelBuilder.Entity<Author>()
            .HasData(authors);
        modelBuilder.Entity<Publisher>()
            .HasData(publishers);
        modelBuilder.Entity<Book>()
            .HasData(books);
        modelBuilder.Entity<AuthorBookAssociation>()
            .HasData(authorBooksAssociations);

        modelBuilder.Entity<BookReview>()
            .HasData(bookReviews);

        modelBuilder.Entity<Address>()
            .HasData(addresses);
        modelBuilder.Entity<BookStore>()
            .HasData(bookStores);
        modelBuilder.Entity<InventoryItem>()
            .HasData(inventoryItems);

        modelBuilder.Entity<WishList>()
            .HasData(wishLists);
        modelBuilder.Entity<WishListItem>()
            .HasData(wishListItems);

        modelBuilder.Entity<Order>()
            .HasData(orders);
        modelBuilder.Entity<OrderItem>()
            .HasData(orderItems);

        SeedUsersAndRoles(modelBuilder, users);
    }

    public static void SeedUsersAndRoles(this ModelBuilder builder, IEnumerable<User> defaultUsers)
    {
        var roles = new List<IdentityRole<long>>()
        {
           new IdentityRole<long>()
           {
                Id = 1,
                Name = UserRole.Admin.ToString(),
                NormalizedName = UserRole.Admin.ToString().ToUpper()
           },
           new IdentityRole<long>()
           {
                Id = 2,
                Name = UserRole.Manager.ToString(),
                NormalizedName = UserRole.Manager.ToString().ToUpper()
           },
           new IdentityRole<long>()
           {
                Id = 3,
                Name = UserRole.User.ToString(),
                NormalizedName = UserRole.User.ToString().ToUpper()
           },
        };
        builder.Entity<IdentityRole<long>>().HasData(roles);


        var passwordHasher = new PasswordHasher<User>();
        foreach (var user in defaultUsers)
        {
            user.Email = user.UserName + "@mail.com";
            user.NormalizedUserName = user.UserName?.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();
            user.PasswordHash = passwordHasher.HashPassword(user, "password");
            user.SecurityStamp = Guid.NewGuid().ToString();
        }
        builder.Entity<User>().HasData(defaultUsers);


        List<IdentityUserRole<long>> userRoles = new List<IdentityUserRole<long>>();
        foreach (var user in defaultUsers)
        {
            userRoles.Add(new IdentityUserRole<long>
            {
                UserId = user.Id,
                RoleId = roles.First(q => q.Name == defaultUsers.First(u => user.Id == u.Id).Role.ToString()).Id
            });
        }
        builder.Entity<IdentityUserRole<long>>().HasData(userRoles);
    }

    private static List<Publisher> PreparePublisherModels()
    {
        return new List<Publisher>()
        {
            new Publisher()
            {
                Id = 1,
                Name = "CreateSpace Independent Publishing Platform",
                City = "London",
                Country = "United Kingdom",
                YearFounded = 2008
            },
            new Publisher()
            {
                Id = 2,
                Name = "Packt Publishing",
                City = "London",
                Country = "United Kingdom",
                YearFounded = 2003
            },
            new Publisher()
            {
                Id = 3,
                Name = "Scholastic",
                City = "New York",
                Country = "United States",
                YearFounded = 1995
            },
            new Publisher()
            {
                Id = 4,
                Name = "Next door Publishing",
                Country = "France",
                YearFounded = 2022
            },
            new Publisher()
            {
                Id = 5,
                Name = "Matej K.",
                City = "Nove Zamky",
                Country = "Slovakia",
                YearFounded = 2020
            },
            new Publisher()
            {
                Id = 6,
                Name = "World Wide Publishing",
                City = "Los Angeles",
                Country = "United States",
                YearFounded = 2005
            },
            new Publisher()
            {
                Id = 7,
                Name = "Facebook Publishing Company",
                City = "Mark",
                Country = "Zuckerbergland",
                YearFounded = 890
            },
            new Publisher()
            {
                Id = 8,
                Name = "Hachette UK",
                YearFounded = 1989
            },
            new Publisher()
            {
                Id = 9,
                Name = "Horizon Publications",
                City = "Tokyo",
                Country = "Japan",
                YearFounded = 2013
            },
            new Publisher()
            {
                Id = 10,
                Name = "Enigma Press",
                City = "Berlin",
                Country = "Germany",
                YearFounded = 1942
            },
            new Publisher()
            {
                Id = 11,
                Name = "Addison-Wesley Professional",
                Country = "Ireland",
                YearFounded = 2018
            },
            new Publisher()
            {
                Id = 12,
                Name = "DC Comics",
                City = "Las Vegas",
                Country = "United States",
                YearFounded = 2010
            }
        };
    }

    private static List<Author> PrepareAuthorModels()
    {
        return new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Jamie Chan",
                Biography = "Get to know me, pal"
            },
            new Author()
            {
                Id = 2,
                Name = "Jackie Chan",
                Biography = "Kung-fu movie legend from China"
            },
            new Author()
            {
                Id = 3,
                Name = "Rafal Swidzinski",
                Biography = "Once upon a time I was dreaming of being a book author"
            },
            new Author()
            {
                Id = 4,
                Name = "J.K. Rowling",
                Biography = "Enchanting readers with magical worlds in Harry Potter series, a testament to creativity and resilience"
            },
            new Author()
            {
                Id = 5,
                Name = "Jack Sparknotes",
                Biography = "Pioneer of science fiction, exploring robotics and future societies through visionary storytelling"
            },
            new Author()
            {
                Id = 6,
                Name = "Matej K.",
                Biography = "The great one"
            },
            new Author()
            {
                Id = 7,
                Name = "K. Racer",
                Biography = "Poetic voice of resilience and hope, inspiring through eloquence and empowerment"
            },
            new Author()
            {
                Id = 8,
                Name = "Mark Zuckerberg",
                Biography = "Born as a man that fights with lizzard powers"
            },
            new Author()
            {
                Id = 9,
                Name = "Heisenberg",
                Biography = "Say my name!"
            },
            new Author()
            {
                Id = 10,
                Name = "James R. Anderson",
                Biography = "Himsical wordsmith, sparking imagination with playful rhymes and unforgettable characters"
            },
            new Author()
            {
                Id = 11,
                Name = "Bjarne Stroustrup",
                Biography = "Master of horror, crafting chilling tales that delve into the human psyche and fears"
            },
            new Author()
            {
                Id = 12,
                Name = "Frank Miller"
            },
            new Author()
            {
                Id = 13,
                Name = "Alan Moore"
            }
        };
    }

    private static List<AuthorBookAssociation> PrepareAuthorBooksAssociationsModels()
    {
        return new List<AuthorBookAssociation>()
        {
            new AuthorBookAssociation()
            {
                Id = 1,
                AuthorId = 1,
                BookId = 1,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 2,
                AuthorId = 2,
                BookId = 1,
                IsPrimary = false
            },
            new AuthorBookAssociation()
            {
                Id = 3,
                AuthorId = 3,
                BookId = 2,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 4,
                AuthorId = 4,
                BookId = 3,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 5,
                AuthorId = 4,
                BookId = 4,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 6,
                AuthorId = 4,
                BookId = 5,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 7,
                AuthorId = 4,
                BookId = 6,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 8,
                AuthorId = 4,
                BookId = 7,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 9,
                AuthorId = 4,
                BookId = 8,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 10,
                AuthorId = 4,
                BookId = 9,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 11,
                AuthorId = 5,
                BookId = 10,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 12,
                AuthorId = 5,
                BookId = 11,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 13,
                AuthorId = 6,
                BookId = 12,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 14,
                AuthorId = 7,
                BookId = 13,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 15,
                AuthorId = 8,
                BookId = 14,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 16,
                AuthorId = 9,
                BookId = 15,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 17,
                AuthorId = 10,
                BookId = 17,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 18,
                AuthorId = 11,
                BookId = 18,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 19,
                AuthorId = 12,
                BookId = 19,
                IsPrimary = true
            },
            new AuthorBookAssociation()
            {
                Id = 20,
                AuthorId = 13,
                BookId = 20,
                IsPrimary = true
            },
        };
    }

    private static List<BookReview> PrepareBookReviews()
    {
        return new List<BookReview>
        {
        new BookReview
        {
            Id = 1,
            BookId = 1,
            ReviewerId = 3,
            Description = "A great book to start learning C#. It covers all the fundamentals and provides clear explanations. Highly recommended for beginners.",
            Rating = Rating.Excellent
        },
        new BookReview
        {
            Id = 2,
            BookId = 2,
            ReviewerId = 5,
            Description = "This book on modern CMake is a game-changer. It's well-structured and makes complex concepts easy to understand. A must-read for C++ developers.",
            Rating = Rating.Excellent
        },
        new BookReview
        {
            Id = 3,
            BookId = 3,
            ReviewerId = 4,
            Description = "Harry Potter is a classic, and this book is where the magical journey begins. It's a must-read for fantasy lovers of all ages.",
            Rating = Rating.Good
        },
        new BookReview
        {
            Id = 4,
            BookId = 4,
            ReviewerId = 7,
            Description = "The second book in the Harry Potter series continues to captivate readers with its magic and mystery. A great follow-up to the first book.",
            Rating = Rating.Good
        },
        new BookReview
        {
            Id = 5,
            BookId = 5,
            ReviewerId = 6,
            Description = "The third book in the Harry Potter series introduces new elements to the story, making it even more exciting. J.K. Rowling's writing is exceptional.",
            Rating = Rating.Excellent
        },
        new BookReview
        {
            Id = 6,
            BookId = 6,
            ReviewerId = 3,
            Description = "Another fantastic addition to the Harry Potter series. The Goblet of Fire is full of surprises and keeps you engaged throughout.",
            Rating = Rating.Excellent
        },
        new BookReview
        {
            Id = 7,
            BookId = 7,
            ReviewerId = 4,
            Description = "The Order of the Phoenix takes the series to a darker place. It's a compelling read with intense moments and character development.",
            Rating = Rating.Good
        },
        new BookReview
        {
            Id = 8,
            BookId = 8,
            ReviewerId = 6,
            Description = "The Half-Blood Prince is a pivotal book in the Harry Potter series. It reveals crucial information and sets the stage for the final showdown.",
            Rating = Rating.Excellent
        },
        new BookReview
        {
            Id = 9,
            BookId = 9,
            ReviewerId = 7,
            Description = "A thrilling conclusion to the Harry Potter saga. All loose ends are tied up, and it's an emotional journey for readers.",
            Rating = Rating.Excellent
        },
        new BookReview
        {
            Id = 10,
            BookId = 10,
            ReviewerId = 5,
            Description = "A mysterious door leads to an intriguing plot. 'Behind the Real Door' is a gripping mystery novel that keeps you guessing until the end.",
            Rating = Rating.Good
        },
         new BookReview
        {
            Id = 11,
            BookId = 11,
            ReviewerId = 3,
            Description = "I didn't enjoy this book at all. The plot was confusing, and the characters were unrelatable. Would not recommend.",
            Rating = Rating.Terrible
        },
        new BookReview
        {
            Id = 12,
            BookId = 12,
            ReviewerId = 4,
            Description = "I found 'Culinary Delights' to be underwhelming. The recipes were basic, and I was expecting more innovative dishes.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 13,
            BookId = 13,
            ReviewerId = 5,
            Description = "This book was a disappointment. The writing style was dry, and the characters lacked depth. Not recommended.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 14,
            BookId = 14,
            ReviewerId = 6,
            Description = "I couldn't finish 'How to teach kids to share.' It felt like a poorly written joke. Save your money and time.",
            Rating = Rating.Terrible
        },
        new BookReview
        {
            Id = 15,
            BookId = 15,
            ReviewerId = 7,
            Description = "I expected 'Elemental' to be an exciting exploration of meteorology, but it was overly technical and lacked engaging content.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 16,
            BookId = 16,
            ReviewerId = 3,
            Description = "Despite its title, 'Eternal Skies' failed to deliver a captivating narrative about meteorology. It felt dull and uninspiring.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 17,
            BookId = 17,
            ReviewerId = 4,
            Description = "I had high hopes for 'Secrets of the Lost Scroll,' but the plot was convoluted, and the characters were forgettable.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 18,
            BookId = 18,
            ReviewerId = 5,
            Description = "I expected 'Tour of C++' to be a comprehensive guide, but it felt disjointed and lacked clear explanations. Disappointing.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 19,
            BookId = 19,
            ReviewerId = 6,
            Description = "Batman: Year One was not as engaging as I hoped. The storyline was uninspiring, and the artwork didn't impress me.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 20,
            BookId = 20,
            ReviewerId = 7,
            Description = "I found 'Batman the Killing Joke' to be too violent and disturbing for my taste. It didn't live up to the hype.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 27,
            BookId = 8,
            ReviewerId = 8,
            Description = "I had high expectations for 'Harry Potter and the Half-Blood Prince,' but I found it lacking in depth compared to the previous books in the series.",
            Rating = Rating.Fair
        },
        new BookReview
        {
            Id = 28,
            BookId = 9,
            ReviewerId = 9,
            Description = "A thrilling conclusion to the Harry Potter series. It answers many questions and provides a satisfying end to the story. Highly recommended.",
            Rating = Rating.Fair
        },
        new BookReview
        {
            Id = 29,
            BookId = 10,
            ReviewerId = 10,
            Description = "I thoroughly enjoyed 'Behind the Real Door.' The plot was intriguing, and it kept me guessing until the end. A must-read mystery novel.",
            Rating = Rating.Good
        },
        new BookReview
        {
            Id = 30,
            BookId = 18,
            ReviewerId = 11,
            Description = "As a C++ developer, I expected more from 'Tour of C++.' It felt incomplete and lacked the depth I needed for advanced programming.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 31,
            BookId = 14,
            ReviewerId = 12,
            Description = "I didn't find 'How to teach kids to share' engaging or informative. It failed to deliver practical advice for teaching children about sharing.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 32,
            BookId = 15,
            ReviewerId = 13,
            Description = "I was disappointed with 'Elemental.' It promised to simplify chemistry but ended up being overly technical. Not a recommended read.",
            Rating = Rating.Poor
        },
        new BookReview
        {
            Id = 33,
            BookId = 11,
            ReviewerId = 14,
            Description = "I found 'Whispers in the Shadows' to be a captivating mystery novel. It kept me hooked with its eerie atmosphere and intriguing plot.",
            Rating = Rating.Good
        }
    };
    }

    private static List<User> PrepareUserModels()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "Housemaster111",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 2,
                UserName = "olivia.johnson",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 3,
                UserName = "liamthereaded",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 4,
                UserName = "emily_in_paris",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 5,
                UserName = "booklover88",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 6,
                UserName = "maplewoodhighschool",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 7,
                UserName = "PeterParker",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 8,
                UserName = "codingWizard42",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 9,
                UserName = "bookworm",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 10,
                UserName = "22avidReader22",
                Role = UserRole.Manager
            },
            new User
            {
                Id = 11,
                UserName = "programmingGuru",
                Role = UserRole.User
            },
            new User
            {
                Id = 12,
                UserName = "mysteryFanatic",
                Role = UserRole.User
            },
            new User
            {
                Id = 13,
                UserName = "techEnthusiast",
                Role = UserRole.User
            },
            new User
            {
                Id = 14,
                UserName = "foodLover88",
                Role = UserRole.User
            },
            new User
            {
                Id = 15,
                UserName = "john_the_ipper",
                Role = UserRole.User
            },
            new User
            {
                Id = 16,
                UserName = "samuel_ackson",
                Role = UserRole.User
            },
            new User
            {
                Id = 17,
                UserName = "admin",
                Role = UserRole.Admin
            },
        };
    }

    private static List<Book> PrepareBookModels()
    {
        return new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Learn C# in One Day and Learn It Well",
                ISBN = "978-1518800276",
                PublisherId = 1,
                BookGenre = Models.Enums.BookGenre.Programming,
                Description = "Have you always wanted to learn computer programming but are afraid it'll be too difficult for you? " +
                "Or perhaps you know other programming languages but are interested in learning the C# language fast? This book is for " +
                "you. You no longer have to waste your time and money learning C# from boring books that are 600 pages long, expensive " +
                "online courses or complicated C# tutorials that just leave you more confused.",
                Price = 10.58
            },
            new Book
            {
                Id = 2,
                Title = "Modern CMake for C++: Discover a better approach to building, testing, and packaging your software",
                ISBN = "978-1801070058",
                PublisherId = 2,
                BookGenre = Models.Enums.BookGenre.Programming,
                Description = "Creating top-notch software is an extremely difficult undertaking. Developers researching the subject have " +
                "difficulty determining which advice is up to date and which approaches have already been replaced by easier, better practices. " +
                "At the same time, most online resources offer limited explanation, while also lacking the proper context and structure.",
                Price = 35.99
            },
            new Book
            {
                Id = 3,
                Title = "Harry Potter and the Sorcerer's Stone",
                ISBN = "978-1338878929",
                PublisherId = 3,
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "Harry Potter has no idea how famous he is. That's because he's being raised by his miserable aunt and uncle who" +
                " are terrified Harry will learn that he's really a wizard, just as his parents were. But everything changes when Harry is summoned " +
                "to attend an infamous school for wizards, and he begins to discover some clues about his illustrious birthright. From the " +
                "surprising way he is greeted by a lovable giant, to the unique curriculum and colorful faculty at his unusual school, " +
                "Harry finds himself drawn deep inside a mystical world he never knew existed and closer to his own noble destiny.",
                Price = 6.80
            },
            new Book
            {
                Id = 4,
                Title = "Harry Potter and the Chamber of Secrets",
                ISBN = "978-1338878936",
                PublisherId = 3,
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "The Dursleys were so mean and hideous that summer that all Harry Potter wanted was to get back to the " +
                "Hogwarts School for Witchcraft and Wizardry. But just as he's packing his bags, Harry receives a warning from a " +
                "strange, impish creature named Dobby who says that if Harry Potter returns to Hogwarts, disaster will strike. And strike " +
                "it does. For in Harry's second year at Hogwarts, fresh torments and horrors arise, including an outrageously stuck-up " +
                "new professor, Gilderoy Lockhart, a spirit named Moaning Myrtle who haunts the girls' bathroom, and the unwanted " +
                "attentions of Ron Weasley's younger sister, Ginny.",
                Price = 6.30
            },
            new Book
            {
                Id = 5,
                Title = "Harry Potter and the Prisoner of Azkaban",
                ISBN = "978-1338299168",
                PublisherId = 3,
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "For twelve long years, the dread fortress of Azkaban held an infamous prisoner named Sirius Black. " +
                "Convicted of killing thirteen people with a single curse, he was said to be the heir apparent to the Dark Lord, " +
                "Voldemort.Now he has escaped, leaving only two clues as to where he might be headed: Harry Potter's defeat of " +
                "You-Know-Who was Black's downfall as well. And the Azkaban guards heard Black muttering in his sleep, \"He's at " +
                "Hogwarts... he's at Hogwarts.\"Harry Potter isn't safe, not even within the walls of his magical school, " +
                "surrounded by his friends. Because on top of it all, there may be a traitor in their midst.",
                Price = 8.20
            },
            new Book
            {
                Id = 6,
                Title = "Harry Potter and the Goblet of Fire",
                ISBN = "978-1338878950",
                PublisherId = 3,
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "Harry wants to get away from the pernicious Dursleys and go to the International Quidditch Cup with " +
                "Hermione, Ron, and the Weasleys. He wants to dream about Cho Chang, his crush (and maybe do more than dream). " +
                "He wants to find out about the mysterious event involving two other rival schools of magic, and a competition " +
                "that hasn't happened for a hundred years. He wants to be a normal, fourteen-year-old wizard. Unfortunately " +
                "for Harry Potter, he's not normal - even by wizarding standards.And in this case, different can be deadly.",
                Price = 11.99
            },
            new Book
            {
                Id = 7,
                Title = "Harry Potter and the Order of the Phoenix",
                ISBN = "978-1338299182",
                PublisherId = 3,
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "There is a door at the end of a silent corridor. And it's haunting Harry Potter's dreams. Why else would " +
                "he be waking in the middle of the night, screaming in terror?It's not just the upcoming O.W.L. exams; a new teacher with " +
                "a personality like poisoned honey; a venomous, disgruntled house-elf; or even the growing threat of He-Who-Must-Not-Be-Named. " +
                "Now Harry Potter is faced with the unreliability of the very government of the magical world and the impotence of the " +
                "authorities at Hogwarts.Despite this (or perhaps because of it), he finds depth and strength in his friends, beyond what " +
                "even he knew; boundless loyalty; and unbearable sacrifice.",
                Price = 9.30
            },
            new Book
            {
                Id = 8,
                Title = "Harry Potter and the Half-Blood Prince",
                ISBN = "978-1338878974",
                PublisherId = 3,
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "The war against Voldemort is not going well; even Muggle governments are noticing. Ron scans the obituary " +
                "pages of the Daily Prophet, looking for familiar names. Dumbledore is absent from Hogwarts for long stretches of time, and " +
                "the Order of the Phoenix has already suffered losses. And yet... As in all wars, life goes on. Sixth-year students learn to " +
                "Apparate - and lose a few eyebrows in the process. The Weasley twins expand their business. Teenagers flirt and fight and fall " +
                "in love. Classes are never straightforward, though Harry receives some extraordinary help from the mysterious Half-Blood Prince.",
                Price = 9.35
            },
            new Book
            {
                Id = 9,
                Title = "Harry Potter and the Deathly Hallows",
                PublisherId = 3,
                ISBN = "978-1408855713",
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "Will Harry die in the final battle against the mighty Voldemort? Maybe. Just read the book and you will find out.",
                Price = 8.95
            },
            new Book
            {
                Id = 10,
                Title = "Behind the real door",
                PublisherId = 4,
                ISBN = "121-1409055700",
                BookGenre = Models.Enums.BookGenre.Mystery,
                Description = "Gripping mystery novel that delves into a world of secrets, where a seemingly ordinary door conceals a " +
                "web of intrigue, unsolved crimes, and dark truths. As the detective Mike unravels the mysteries lurking \"behind the real " +
                "door,\" the line between reality and illusion blurs, keeping readers on the edge of their seats until the shocking climax.",
                Price = 120.20
            },
            new Book
            {
                Id = 11,
                Title = "Whispers in the Shadows",
                ISBN = "121-1409055701",
                PublisherId = 4,
                BookGenre = Models.Enums.BookGenre.Mystery,
                Description = "In 'Whispers in the Shadows,' a small, enigmatic town hides dark secrets. As a series of eerie whispers and " +
                "unexplained phenomena plague its residents, a determined investigator Mike must unearth the truth, facing a tangled web of " +
                "deceit, paranormal occurrences, and a past that refuses to stay buried. With every page, the mystery deepens, keeping " +
                "readers spellbound.",
                Price = 119.99
            },
            new Book
            {
                Id = 12,
                Title = "Memoirs of the Matej K., the great one",
                ISBN = "420-4204204200",
                PublisherId = 5,
                BookGenre = Models.Enums.BookGenre.Memoir,
                Description = "Matej K., one of the greatest programmers and mathematicians of all time is sharing some of his knowledge " +
                "with the readers. In his endles list of successes he helped Elon Musk build the ship to Mars, ended the world hunger and " +
                "created a completly new approach to AI. Join him in this journey accross universes and take a glimpse look into his life " +
                "full of interesting events.",
                Price = 30.99
            },
            new Book
            {
                Id = 13,
                Title = "Culinary Delights",
                ISBN = "321-7503791824",
                PublisherId = 6,
                BookGenre = Models.Enums.BookGenre.Cooking,
                Description = "Discover a world of gastronomic pleasures in 'Culinary Delights.' This recipe book takes you on a mouthwatering " +
                "journey through diverse cuisines, offering a delightful array of dishes from appetizers to desserts. Whether you're a novice " +
                "cook or a seasoned chef, you'll find inspiration and easy-to-follow recipes that will tantalize your taste buds and elevate " +
                "your culinary skills.",
                Price = 15
            },
            new Book
            {
                Id = 14,
                Title = "How to teach kids to share",
                ISBN = "816-0815794691",
                PublisherId = 7,
                BookGenre = Models.Enums.BookGenre.Parenting,
                Description = "Hi, my name is Mark, the CEO of Meta. Eons ago I created a funny app for sharing called facebook. In this book I " +
                "will present to you, my audience (or simply my subjects), how we, the lizzardmen, are sharing data. This guide is mainly for children.",
                Price = 0.50
            },
            new Book
            {
                Id = 15,
                Title = "Elemental: How the Periodic Table Can Now Explain (Nearly) Everything",
                ISBN = "936-7213567800",
                PublisherId = 8,
                BookGenre = Models.Enums.BookGenre.Science,
                Description = "From the periodic table to chemical reactions, this book demystifies the complexities of chemistry. Engaging explanations " +
                "and real-world applications make it a captivating journey through the science that shapes our daily lives, from the laboratory " +
                "to the natural world.",
                Price = 9.99
            },
            new Book
            {
                Id = 16,
                Title = "Eternal Skies",
                ISBN = "978-1234567890",
                PublisherId = 9,
                BookGenre = Models.Enums.BookGenre.Science,
                Description = "Join Sarah on a breathtaking adventure to unravel the mysteries of the skies. \"Eternal Skies\" explores meteorology and " +
                "the wonders of the atmosphere, revealing the secrets hidden in every cloud and the magic of the ever-changing weather. A perfect guide " +
                "for aspiring meteorologists and weather enthusiasts.",
                Price = 24.99
            },
            new Book
            {
                Id = 17,
                Title = "Secrets of the Lost Scroll",
                ISBN = "111-2850195739",
                PublisherId = 10,
                BookGenre = Models.Enums.BookGenre.Science,
                Description = "Dr. Amelia Stanton embarks on a thrilling archaeological quest to decode an ancient scroll. Uncover lost civilizations, " +
                "hidden treasures, and cryptic messages as you follow her journey through time and space in \"Secrets of the Lost Scroll.\"",
                Price = 18.95
            },
            new Book
            {
                Id = 18,
                Title = "Tour of C++",
                ISBN = "978-0136816485",
                PublisherId = 11,
                BookGenre = Models.Enums.BookGenre.Programming,
                Description = "Bjarne Stroustrup provides an overview of ISO C++, C++20, that aims to give experienced programmers a clear understanding " +
                "of what constitutes modern C++. Featuring carefully crafted examples and practical help in getting started, this revised and updated " +
                "edition concisely covers most major language features and the major standard-library components needed for effective use.",
                Price = 29.99
            },
            new Book
            {
                Id = 19,
                Title = "Batman: Year One",
                ISBN = "978-0290204890",
                PublisherId = 12,
                BookGenre = Models.Enums.BookGenre.Comics,
                Description = "In 1986, Frank Miller and David Mazzucchelli produced this groundbreaking reinterpretation of the origin of Batman—who he is, " +
                "and how he came to be. Sometimes careless and naive, this Dark Knight is far from the flawless vigilante he is today.In his first year on " +
                "the job, Batman feels his way around a Gotham City far darker than the one he left. His solemn vow to extinguish the town’s criminal element " +
                "is only half the battle; along with Lieutenant James Gordon, the Dark Knight must also fight a police force more corrupt than the scum in " +
                "the streets.",
                Price = 9.29
            },
            new Book
            {
                Id = 20,
                Title = "Batman the Killing Joke: The Deluxe Edition",
                ISBN = "978-1401294052",
                PublisherId = 12,
                BookGenre = Models.Enums.BookGenre.Comics,
                Description = "Now Batman must race to stop his archnemesis before his reign of terror claims two of the Dark Knight's closest friends. " +
                "Can he finally put an end to the cycle of bloodlust and lunacy that links these two iconic foes before it leads to its fatal conclusion? " +
                "And as the horrifying origin of the Clown Prince of Crime is finally revealed, will the thin line that separates Batman's nobility and the " +
                "Joker's insanity snap once and for all?",
                Price = 11.19
            }
        };
    }

    private static List<WishList> PrepareWishListModels()
    {
        return new List<WishList>()
        {
            new WishList
            {
                Id = 1,
                UserId = 1,
                Description = "I'd love to add 'Learn C# in One Day and Learn It Well' by Jamie Chan to my collection. It seems like a concise guide to quickly grasp the concepts of C#."
            },
            new WishList
            {
                Id = 2,
                UserId = 2,
                Description = "The 'Modern CMake for C++' book by Rafal Swidzinski has caught my attention. I've heard it offers a fresh perspective on building and packaging software efficiently."
            },
            new WishList
            {
                Id = 3,
                UserId = 3,
                Description = "I've been thoroughly enjoying the Harry Potter series. Next on my list are 'Harry Potter and the Chamber of Secrets', 'Harry Potter and the Prisoner of Azkaban', and 'Harry Potter and the Goblet of Fire'. Each one promises more exciting adventures and mysteries at Hogwarts. Can't wait to dive into them!"
            },
            new WishList
            {
                Id = 4,
                UserId = 4,
                Description = "Moving away from fantasy, the mystery novel 'Behind the real door' by Jack Sparknotes has been suggested to me. The concept of secrets behind a door sounds like a thrilling read!"
            },
            new WishList
            {
                Id = 5,
                UserId = 5,
                Description = "I'm eager to delve deeper into Batman's lore. 'Batman: Year One' by Frank Miller sounds captivating with its raw and gritty reinterpretation of Batman's origin. I'm also intrigued by 'Batman the Killing Joke: The Deluxe Edition' by Alan Moore. The intense rivalry and the blurred line between Batman and Joker have always fascinated me. Both these masterpieces are must-haves for my collection."
            }
       };
    }

    private static List<WishListItem> PrepareWishListItemModels()
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
                WishListId = 2,
                BookId = 2,
                PreferencePriority = 1,
            },
            new WishListItem
            {
                Id = 3,
                WishListId = 2,
                BookId = 18,
                PreferencePriority = 2,
            },
            new WishListItem
            {
                Id = 4,
                WishListId = 3,
                BookId = 3,
                PreferencePriority = 1,
            },
            new WishListItem
            {
                Id = 5,
                WishListId = 3,
                BookId = 4,
                PreferencePriority = 2,
            },
            new WishListItem
            {
                Id = 6,
                WishListId = 3,
                BookId = 5,
                PreferencePriority = 0,
            },
            new WishListItem
            {
                Id = 7,
                WishListId = 4,
                BookId = 6,
                PreferencePriority = 1,
            },
            new WishListItem
            {
                Id = 8,
                WishListId = 5,
                BookId = 7,
                PreferencePriority = 1,
            }
        };
    }

    private static List<Order> PrepareOrderModels()
    {
        return new List<Order>()
        {
            new Order
            {
                Id = 1,
                UserId = 4,
                State = OrderState.Cancelled
            },
            new Order
            {
                Id = 2,
                UserId = 4,
                State = OrderState.Created
            },
            new Order
            {
                Id = 3,
                UserId = 5,
                State = OrderState.Created
            },
            new Order
            {
                Id = 4,
                UserId = 5,
                State = OrderState.Refunded
            },
            new Order
            {
                Id = 5,
                UserId = 6,
                State = OrderState.Paid
            },
            new Order
            {
                Id = 6,
                UserId = 7,
                State = OrderState.Paid
            },
            new Order
            {
                Id = 7,
                UserId = 7,
                State = OrderState.Cancelled
            },
            new Order
            {
                Id = 8,
                UserId = 7,
                State = OrderState.Cancelled
            },
            new Order
            {
                Id = 9,
                UserId = 7,
                State = OrderState.Created
            },
            new Order
            {
                Id = 10,
                UserId = 8,
                State = OrderState.Paid
            },
            new Order
            {
                Id = 11,
                UserId = 8,
                State = OrderState.Paid
            },
            new Order
            {
                Id = 12,
                UserId = 8,
                State = OrderState.Created
            },
            new Order
            {
                Id = 13,
                UserId = 8,
                State = OrderState.Created
            },
            new Order
            {
                Id = 14,
                UserId = 8,
                State = OrderState.Cancelled
            },
            new Order
            {
                Id = 15,
                UserId = 15,
                State = OrderState.Paid
            },
            new Order
            {
                Id = 16,
                UserId = 15,
                State = OrderState.Paid
            }
        };
    }

    private static List<OrderItem> PrepareOrderItemModels()
    {
        return new List<OrderItem>()
        {
            new OrderItem
            {
                Id = 1,
                BookId = 3,
                BookStoreId = 2,
                OrderId = 2,
                Price = 6.80,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 2,
                BookId = 3,
                BookStoreId = 1,
                OrderId = 3,
                Price = 6.80,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 3,
                BookId = 4,
                BookStoreId = 1,
                OrderId = 3,
                Price = 6.30,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 4,
                BookId = 6,
                BookStoreId = 1,
                OrderId = 4,
                Price = 11.99,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 5,
                BookId = 20,
                BookStoreId = 3,
                OrderId = 5,
                Price = 111.90,
                Quantity = 10
            },
            new OrderItem
            {
                Id = 6,
                BookId = 3,
                BookStoreId = 1,
                OrderId = 6,
                Price = 6.80,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 7,
                BookId = 4,
                BookStoreId = 1,
                OrderId = 6,
                Price = 6.30,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 8,
                BookId = 5,
                BookStoreId = 1,
                OrderId = 6,
                Price = 8.20,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 9,
                BookId = 6,
                BookStoreId = 1,
                OrderId = 6,
                Price = 11.90,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 10,
                BookId = 7,
                BookStoreId = 1,
                OrderId = 6,
                Price = 9.30,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 11,
                BookId = 8,
                BookStoreId = 1,
                OrderId = 6,
                Price = 9.35,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 12,
                BookId = 9,
                BookStoreId = 1,
                OrderId = 6,
                Price = 8.95,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 13,
                BookId = 1,
                BookStoreId = 2,
                OrderId = 10,
                Price = 105.8,
                Quantity = 10
            },
            new OrderItem
            {
                Id = 14,
                BookId = 2,
                BookStoreId = 2,
                OrderId = 10,
                Price = 350.9,
                Quantity = 10
            },
            new OrderItem
            {
                Id = 15,
                BookId = 18,
                BookStoreId = 2,
                OrderId = 10,
                Price = 290.9,
                Quantity = 10
            },
            new OrderItem
            {
                Id = 16,
                BookId = 19,
                BookStoreId = 3,
                OrderId = 11,
                Price = 46.45,
                Quantity = 5
            },
            new OrderItem
            {
                Id = 17,
                BookId = 20,
                BookStoreId = 3,
                OrderId = 11,
                Price = 33.57,
                Quantity = 3
            },
            new OrderItem
            {
                Id = 18,
                BookId = 12,
                BookStoreId = 3,
                OrderId = 12,
                Price = 30.99,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 19,
                BookId = 13,
                BookStoreId = 3,
                OrderId = 12,
                Price = 15,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 20,
                BookId = 10,
                BookStoreId = 1,
                OrderId = 14,
                Price = 120.10,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 21,
                BookId = 11,
                BookStoreId = 1,
                OrderId = 14,
                Price = 119.99,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 22,
                BookId = 14,
                BookStoreId = 1,
                OrderId = 15,
                Price = 0.50,
                Quantity = 1
            },
            new OrderItem
            {
                Id = 23,
                BookId = 16,
                BookStoreId = 2,
                OrderId = 16,
                Price = 24.99,
                Quantity = 1
            },
        };
    }

    private static List<Address> PrepareAddressModels()
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
            new Address
            {
                Id = 3,
                Street = "789 Oak Ave",
                StreetNumber = "Suite 2C",
                City = "Testville",
                State = "TX",
                PostalCode = "45678"
            },
            new Address
            {
                Id = 4,
                Street = "101 Pine Rd",
                StreetNumber = "Apt 6D",
                City = "Mockington",
                State = "FL",
                PostalCode = "90123"
            },
            new Address
            {
                Id = 5,
                Street = "222 Cedar Ln",
                StreetNumber = "Unit 5A",
                City = "Trialsville",
                State = "IL",
                PostalCode = "34567"
            },
            new Address
            {
                Id = 6,
                Street = "333 Birch St",
                StreetNumber = "Suite 3B",
                City = "Sample Springs",
                State = "AZ",
                PostalCode = "78901"
            },
            new Address
            {
                Id = 7,
                Street = "444 Redwood Ave",
                StreetNumber = "Apt 2F",
                City = "Illustration City",
                State = "WA",
                PostalCode = "23456"
            },
            new Address
            {
                Id = 8,
                Street = "555 Sycamore Rd",
                StreetNumber = "Unit 4E",
                City = "Instanceville",
                State = "OR",
                PostalCode = "56789"
            },
            new Address
            {
                Id = 9,
                Street = "666 Elm St",
                StreetNumber = "Suite 1A",
                City = "Demo Town",
                State = "NM",
                PostalCode = "12345"
            },
            new Address
            {
                Id = 10,
                Street = "777 Oak Ave",
                StreetNumber = "Apt 5B",
                City = "Example Springs",
                State = "NV",
                PostalCode = "67890"
            },
            new Address
            {
                Id = 11,
                Street = "888 Maple Ln",
                StreetNumber = "Unit 3C",
                City = "Testington",
                State = "UT",
                PostalCode = "45678"
            }
        };
    }

    private static List<BookStore> PrepareBookStoreModels()
    {
        return new List<BookStore>()
        {
            new BookStore
            {
                Id = 1,
                AddressId = 1,
                ManagerId = 1,
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
            new BookStore
            {
                Id = 3,
                AddressId = 4,
                ManagerId = 3,
                Name = "Book Haven",
                PhoneNumber = "+987 654 3210",
                Email = "bookhaven@store.net",
            },
            new BookStore
            {
                Id = 4,
                AddressId = 5,
                ManagerId = 4,
                Name = "Reading Nook",
                PhoneNumber = "+555 123 4567",
                Email = "nook.reads@bookshop.org",
            },
            new BookStore
            {
                Id = 5,
                AddressId = 6,
                ManagerId = 5,
                Name = "Literary Lighthouse",
                PhoneNumber = "+777 999 8888",
                Email = "litlighthouse@mail.com",
            },
            new BookStore
            {
                Id = 6,
                AddressId = 7,
                ManagerId = 6,
                Name = "Page Turner Books",
                PhoneNumber = "+333 555 7777",
                Email = "pageturner@store.info",
            },
            new BookStore
            {
                Id = 7,
                AddressId = 8,
                ManagerId = 7,
                Name = "Classic Reads",
                PhoneNumber = "+999 111 2222",
                Email = "classicreads@example.com",
            },
            new BookStore
            {
                Id = 8,
                AddressId = 9,
                ManagerId = 8,
                Name = "Book Emporium",
                PhoneNumber = "+123 987 6543",
                Email = "emporium@bookstore.net",
            },
            new BookStore
            {
                Id = 9,
                AddressId = 10,
                ManagerId = 9,
                Name = "Novel Nook",
                PhoneNumber = "+555 777 3333",
                Email = "novelnook@books.biz",
            },
            new BookStore
            {
                Id = 10,
                AddressId = 11,
                ManagerId = 10,
                Name = "PageMaster Books",
                PhoneNumber = "+666 888 2222",
                Email = "pagemaster@example.com",
            }
        };
    }

    private static List<InventoryItem> PrepareInventoryItemModels()
    {
        return new List<InventoryItem>()
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
                BookId = 4,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 5,
                BookId = 5,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 6,
                BookId = 6,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 7,
                BookId = 7,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 8,
                BookId = 8,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 9,
                BookId = 9,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 10,
                BookId = 10,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 11,
                BookId = 11,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 12,
                BookId = 12,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 13,
                BookId = 13,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 14,
                BookId = 14,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 15,
                BookId = 15,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 16,
                BookId = 16,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 17,
                BookId = 17,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 18,
                BookId = 18,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 19,
                BookId = 19,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 20,
                BookId = 20,
                BookStoreId = 1,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 13)
            },
            new InventoryItem
            {
                Id = 21,
                BookId = 1,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 22,
                BookId = 2,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 23,
                BookId = 3,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 24,
                BookId = 4,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 25,
                BookId = 5,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 26,
                BookId = 6,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 27,
                BookId = 7,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 28,
                BookId = 8,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 29,
                BookId = 9,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 30,
                BookId = 10,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 31,
                BookId = 11,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 32,
                BookId = 12,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 33,
                BookId = 13,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 34,
                BookId = 14,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 35,
                BookId = 15,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 36,
                BookId = 16,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 37,
                BookId = 17,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 38,
                BookId = 18,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 39,
                BookId = 19,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 40,
                BookId = 20,
                BookStoreId = 2,
                InStock = 5,
                LastRestock = new DateTime(2023, 8, 15)
            },
            new InventoryItem
            {
                Id = 41,
                BookId = 1,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 42,
                BookId = 2,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 43,
                BookId = 3,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 44,
                BookId = 4,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 45,
                BookId = 5,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 46,
                BookId = 6,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 47,
                BookId = 7,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 48,
                BookId = 8,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 49,
                BookId = 9,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 50,
                BookId = 10,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 51,
                BookId = 11,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 52,
                BookId = 12,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 53,
                BookId = 13,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 54,
                BookId = 14,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 55,
                BookId = 15,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 56,
                BookId = 16,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 57,
                BookId = 17,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 58,
                BookId = 18,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 59,
                BookId = 19,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 60,
                BookId = 20,
                BookStoreId = 3,
                InStock = 2,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 61,
                BookId = 1,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 62,
                BookId = 2,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 63,
                BookId = 3,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 64,
                BookId = 4,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 65,
                BookId = 5,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 66,
                BookId = 6,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 67,
                BookId = 7,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 68,
                BookId = 8,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 69,
                BookId = 9,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 70,
                BookId = 10,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 71,
                BookId = 11,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 72,
                BookId = 12,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 73,
                BookId = 13,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 74,
                BookId = 14,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 75,
                BookId = 15,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 76,
                BookId = 16,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 77,
                BookId = 17,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 78,
                BookId = 18,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 79,
                BookId = 19,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 80,
                BookId = 20,
                BookStoreId = 4,
                InStock = 7,
                LastRestock = new DateTime(2023, 7, 23)
            },
            new InventoryItem
            {
                Id = 81,
                BookId = 1,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 82,
                BookId = 2,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 83,
                BookId = 3,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 84,
                BookId = 4,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 85,
                BookId = 5,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 86,
                BookId = 6,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 87,
                BookId = 7,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 88,
                BookId = 8,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 89,
                BookId = 9,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 90,
                BookId = 10,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 91,
                BookId = 11,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 92,
                BookId = 12,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 93,
                BookId = 13,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 94,
                BookId = 14,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 95,
                BookId = 15,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 96,
                BookId = 16,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 97,
                BookId = 17,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 98,
                BookId = 18,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 99,
                BookId = 19,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            },
            new InventoryItem
            {
                Id = 100,
                BookId = 20,
                BookStoreId = 5,
                InStock = 10,
                LastRestock = new DateTime(2022, 2, 2)
            }
        };
    }
}
