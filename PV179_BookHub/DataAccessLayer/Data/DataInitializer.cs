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
            new Book
            {
                Id = 1,
                Title = "Learn C# in One Day and Learn It Well",
                Author = "Jamie Chan",
                ISBN = "978-1518800276",
                Publisher = "CreateSpace Independent Publishing Platform",
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
                Author = "Rafal Swidzinski",
                ISBN = "978-1801070058",
                Publisher = "Packt Publishing",
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
                Author = "J.K. Rowling",
                ISBN = "978-1338878929",
                Publisher = "Scholastic",
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
                Author = "J.K. Rowling",
                ISBN = "978-1338878936",
                Publisher = "Scholastic",
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
                Author = "J.K. Rowling",
                ISBN = "978-1338299168",
                Publisher = "Scholastic",
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
                Author = "J.K. Rowling",
                ISBN = "978-1338878950",
                Publisher = "Scholastic",
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
                Author = "J.K. Rowling",
                ISBN = "978-1338299182",
                Publisher = "Scholastic",
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
                Author = "J.K. Rowling",
                ISBN = "978-1338878974",
                Publisher = "Scholastic",
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
                Author = "J.K. Rowling",
                ISBN = "978-1408855713",
                Publisher = "Scholastic",
                BookGenre = Models.Enums.BookGenre.Fantasy,
                Description = "Will Harry die in the final battle against the mighty Voldemort? Maybe. Just read the book and you will find out.",
                Price = 8.95
            },
            new Book
            {
                Id = 10,
                Title = "Behind the real door",
                Author = "Jack Sparknotes",
                ISBN = "121-1409055700",
                Publisher = "Next door Publishing",
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
                Author = "Jack Sparknotes",
                ISBN = "121-1409055701",
                Publisher = "Next door Publishing",
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
                Author = "Matej K.",
                ISBN = "420-4204204200",
                Publisher = "Matej K.",
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
                Author = "K. Racer",
                ISBN = "321-7503791824",
                Publisher = "World Wide Publishing",
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
                Author = "Mark Zuckerberg",
                ISBN = "816-0815794691",
                Publisher = "Facebook Publishing Company",
                BookGenre = Models.Enums.BookGenre.Parenting,
                Description = "Hi, my name is Mark, the CEO of Meta. Eons ago I created a funny app for sharing called facebook. In this book I " +
                "will present to you, my audience (or simply my subjects), how we, the lizzardmen, are sharing data. This guide is mainly for children.",
                Price = 0.50
            },
            new Book
            {
                Id = 15,
                Title = "Elemental: How the Periodic Table Can Now Explain (Nearly) Everything",
                Author = "Heisenberg",
                ISBN = "936-7213567800",
                Publisher = "Hachette UK",
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
                Author = "Samantha Mitchell",
                ISBN = "978-1234567890",
                Publisher = "Horizon Publications",
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
                Author = "James R. Anderson",
                ISBN = "111-2850195739",
                Publisher = "Enigma Press",
                BookGenre = Models.Enums.BookGenre.Science,
                Description = "Dr. Amelia Stanton embarks on a thrilling archaeological quest to decode an ancient scroll. Uncover lost civilizations, " +
                "hidden treasures, and cryptic messages as you follow her journey through time and space in \"Secrets of the Lost Scroll.\"",
                Price = 18.95
            },
            new Book
            {
                Id = 18,
                Title = "Tour of C++",
                Author = "Bjarne Stroustrup",
                ISBN = "978-0136816485",
                Publisher = "Addison-Wesley Professional",
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
                Author = "Frank Miller",
                ISBN = "978-0290204890",
                Publisher = "DC Comics",
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
                Author = "Alan Moore",
                ISBN = "978-1401294052",
                Publisher = "DC Comics",
                BookGenre = Models.Enums.BookGenre.Comics,
                Description = "Now Batman must race to stop his archnemesis before his reign of terror claims two of the Dark Knight's closest friends. " +
                "Can he finally put an end to the cycle of bloodlust and lunacy that links these two iconic foes before it leads to its fatal conclusion? " +
                "And as the horrifying origin of the Clown Prince of Crime is finally revealed, will the thin line that separates Batman's nobility and the " +
                "Joker's insanity snap once and for all?",
                Price = 11.19
            }
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
