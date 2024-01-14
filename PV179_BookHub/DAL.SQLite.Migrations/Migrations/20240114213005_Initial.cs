using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    StreetNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Biography = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    YearFounded = table.Column<ushort>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    PublisherId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookGenre = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 700, nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BookStores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AddressId = table.Column<long>(type: "INTEGER", nullable: false),
                    ManagerId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookStores_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookStores_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBookAssociations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsPrimary = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBookAssociations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorBookAssociations_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBookAssociations_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    ReviewerId = table.Column<long>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookReviews_Users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookStoreId = table.Column<long>(type: "INTEGER", nullable: false),
                    InStock = table.Column<uint>(type: "INTEGER", nullable: false),
                    LastRestock = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItems_BookStores_BookStoreId",
                        column: x => x.BookStoreId,
                        principalTable: "BookStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookStoreId = table.Column<long>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Quantity = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_BookStores_BookStoreId",
                        column: x => x.BookStoreId,
                        principalTable: "BookStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WishListId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    PreferencePriority = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListItem_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItem_WishList_WishListId",
                        column: x => x.WishListId,
                        principalTable: "WishList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "PostalCode", "State", "Street", "StreetNumber" },
                values: new object[,]
                {
                    { 1L, "Brno", "602 00", "Czech Republic", "Palackého třída", "191/241" },
                    { 2L, "Sampletown", "67890", "NY", "456 Elm St", "Unit 7" },
                    { 3L, "Testville", "45678", "TX", "789 Oak Ave", "Suite 2C" },
                    { 4L, "Mockington", "90123", "FL", "101 Pine Rd", "Apt 6D" },
                    { 5L, "Trialsville", "34567", "IL", "222 Cedar Ln", "Unit 5A" },
                    { 6L, "Sample Springs", "78901", "AZ", "333 Birch St", "Suite 3B" },
                    { 7L, "Illustration City", "23456", "WA", "444 Redwood Ave", "Apt 2F" },
                    { 8L, "Instanceville", "56789", "OR", "555 Sycamore Rd", "Unit 4E" },
                    { 9L, "Demo Town", "12345", "NM", "666 Elm St", "Suite 1A" },
                    { 10L, "Example Springs", "67890", "NV", "777 Oak Ave", "Apt 5B" },
                    { 11L, "Testington", "45678", "UT", "888 Maple Ln", "Unit 3C" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", null, "Manager", "MANAGER" },
                    { "ad91e7c3-e544-4087-a661-0e5af1e69bc3", null, "Admin", "ADMIN" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Name" },
                values: new object[,]
                {
                    { 1L, "Get to know me, pal", "Jamie Chan" },
                    { 2L, "Kung-fu movie legend from China", "Jackie Chan" },
                    { 3L, "Once upon a time I was dreaming of being a book author", "Rafal Swidzinski" },
                    { 4L, "Enchanting readers with magical worlds in Harry Potter series, a testament to creativity and resilience", "J.K. Rowling" },
                    { 5L, "Pioneer of science fiction, exploring robotics and future societies through visionary storytelling", "Jack Sparknotes" },
                    { 6L, "The great one", "Matej K." },
                    { 7L, "Poetic voice of resilience and hope, inspiring through eloquence and empowerment", "K. Racer" },
                    { 8L, "Born as a man that fights with lizzard powers", "Mark Zuckerberg" },
                    { 9L, "Say my name!", "Heisenberg" },
                    { 10L, "Himsical wordsmith, sparking imagination with playful rhymes and unforgettable characters", "James R. Anderson" },
                    { 11L, "Master of horror, crafting chilling tales that delve into the human psyche and fears", "Bjarne Stroustrup" },
                    { 12L, null, "Frank Miller" },
                    { 13L, null, "Alan Moore" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "City", "Country", "Name", "YearFounded" },
                values: new object[,]
                {
                    { 1L, "London", "United Kingdom", "CreateSpace Independent Publishing Platform", (ushort)2008 },
                    { 2L, "London", "United Kingdom", "Packt Publishing", (ushort)2003 },
                    { 3L, "New York", "United States", "Scholastic", (ushort)1995 },
                    { 4L, null, "France", "Next door Publishing", (ushort)2022 },
                    { 5L, "Nove Zamky", "Slovakia", "Matej K.", (ushort)2020 },
                    { 6L, "Los Angeles", "United States", "World Wide Publishing", (ushort)2005 },
                    { 7L, "Mark", "Zuckerbergland", "Facebook Publishing Company", (ushort)890 },
                    { 8L, null, null, "Hachette UK", (ushort)1989 },
                    { 9L, "Tokyo", "Japan", "Horizon Publications", (ushort)2013 },
                    { 10L, "Berlin", "Germany", "Enigma Press", (ushort)1942 },
                    { 11L, null, "Ireland", "Addison-Wesley Professional", (ushort)2018 },
                    { 12L, "Las Vegas", "United States", "DC Comics", (ushort)2010 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Role", "UserName" },
                values: new object[,]
                {
                    { 1L, 1, "Housemaster111" },
                    { 2L, 1, "olivia.johnson" },
                    { 3L, 1, "liamthereaded" },
                    { 4L, 1, "emily_in_paris" },
                    { 5L, 1, "booklover88" },
                    { 6L, 1, "maplewoodhighschool" },
                    { 7L, 1, "PeterParker" },
                    { 8L, 1, "codingWizard42" },
                    { 9L, 1, "bookworm" },
                    { 10L, 1, "22avidReader22" },
                    { 11L, 0, "programmingGuru" },
                    { 12L, 0, "mysteryFanatic" },
                    { 13L, 0, "techEnthusiast" },
                    { 14L, 0, "foodLover88" },
                    { 15L, 0, "john_the_ipper" },
                    { 16L, 0, "samuel_ackson" },
                    { 17L, 2, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[,]
                {
                    { "0afb91aa-9fb2-4171-ab18-9f237a7a0501", 0, "062b799b-88f3-43d7-8583-444e69d71cdd", "LocalIdentityUser", "admin@mail.com", false, false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEJfCvpz1ouv5LbCEbQ6pqSOhFgMZ+miDyejjcoW0NCUzeEcXZW4fAUeqA6vKI6Q2dA==", null, false, "6f61eaa1-8ab0-4879-9a8f-25eb49bdfded", false, 17L, "admin" },
                    { "16f0fb39-cbd0-4264-900f-e94f57c07f20", 0, "27cf9fc9-0473-44f6-a41e-6e214961bac8", "LocalIdentityUser", "bookworm@mail.com", false, false, null, "BOOKWORM@MAIL.COM", "BOOKWORM", "AQAAAAIAAYagAAAAELAlEvjh0XVIQHOV/4nrZXidR/wpTDuIs9gPKf5lH6+UPWfQ5H9WvNRLA95O16eqFQ==", null, false, "52fe8c84-883e-4ddc-afe6-d56f9989f2da", false, 9L, "bookworm" },
                    { "38af2f22-852a-4277-aff1-dac5ed58bc8b", 0, "c447af02-6747-425b-9c0f-1ca9460b0f42", "LocalIdentityUser", "liamthereaded@mail.com", false, false, null, "LIAMTHEREADED@MAIL.COM", "LIAMTHEREADED", "AQAAAAIAAYagAAAAECC4tGqn8/L1B+FJ4500PVCA0XU1jqjpAMYvflEqE1il9asr/44GnFc6G+p45PAyKA==", null, false, "92333125-d614-4957-86a0-a15d17a4f461", false, 3L, "liamthereaded" },
                    { "519b8c84-be71-44bb-9ebc-ff3e327aa2d5", 0, "70464f51-0f6e-4d8a-bd30-1139ee34741e", "LocalIdentityUser", "techEnthusiast@mail.com", false, false, null, "TECHENTHUSIAST@MAIL.COM", "TECHENTHUSIAST", "AQAAAAIAAYagAAAAEJWiGqrYdieVMJHnEcQ6YtdgXMwR5tqte5llBcr+adG8arPHQLbZ/SL74Cd8PUjKZw==", null, false, "cffe846b-f020-465f-a1b9-baf090f9d153", false, 13L, "techEnthusiast" },
                    { "51a24170-f3a4-4137-9b8f-e1232a62f925", 0, "a534f83a-cffa-416d-b338-e7dd5e9d2195", "LocalIdentityUser", "codingWizard42@mail.com", false, false, null, "CODINGWIZARD42@MAIL.COM", "CODINGWIZARD42", "AQAAAAIAAYagAAAAEAPEXjgbujoVQVdFtt49HdE1vFELWs4nC+42EolNT785EDJkbqm0SRRMc/VbpgjkuA==", null, false, "514b6f61-ee87-45a7-9a1b-d4d144a3088b", false, 8L, "codingWizard42" },
                    { "5c88336b-8744-4bfa-b741-f0adaa0bddc3", 0, "2446c4d5-b3d4-418c-8242-e31f6bb30152", "LocalIdentityUser", "mysteryFanatic@mail.com", false, false, null, "MYSTERYFANATIC@MAIL.COM", "MYSTERYFANATIC", "AQAAAAIAAYagAAAAEEUrGrcxStT3TZr7uiPZ4OhvdlZARcrROZuriD3PhDjGozEDh8xkd3S3ttaz+KHwPw==", null, false, "839beaab-8091-4821-9098-acb03bcb89b8", false, 12L, "mysteryFanatic" },
                    { "67a545ea-5107-456e-8cf6-93a3d9f10612", 0, "5768dc87-967b-4425-b849-2a7d2e6a8f3b", "LocalIdentityUser", "PeterParker@mail.com", false, false, null, "PETERPARKER@MAIL.COM", "PETERPARKER", "AQAAAAIAAYagAAAAEEpQYx00Pmv2/E5pFIZt7is/iYXLl7LU21KjN0Ex8BmHJlS8wpHvP0VKr1Gt5aoB0Q==", null, false, "00328cdd-f9ed-4093-824c-6128ca02b79f", false, 7L, "PeterParker" },
                    { "6edc1bd4-676f-4f2e-98c5-e1f777bf1917", 0, "404fb22a-ce69-46aa-9a30-5cbfa5659722", "LocalIdentityUser", "olivia.johnson@mail.com", false, false, null, "OLIVIA.JOHNSON@MAIL.COM", "OLIVIA.JOHNSON", "AQAAAAIAAYagAAAAEEhyrkIilrnW/mQIMP2i3weXlVU7kCKTuVQYw8nZbheOnssXGGGHUXC+WsF1UGntbw==", null, false, "a7630b57-942e-4ef3-9d17-b893c7dea072", false, 2L, "olivia.johnson" },
                    { "76cab806-9f8e-4956-83d2-dbde8f43b39b", 0, "4ab1c66f-d9ce-43c5-9d39-5797b983d75c", "LocalIdentityUser", "samuel_ackson@mail.com", false, false, null, "SAMUEL_ACKSON@MAIL.COM", "SAMUEL_ACKSON", "AQAAAAIAAYagAAAAELG8gOawXevETjxrU9GJ08kPOjTG0PY+bwI9m/zVUgCiCD8DkPUkLN4RhdiBHOrFVQ==", null, false, "827d1873-824e-49dd-9741-9bcbe01eba0d", false, 16L, "samuel_ackson" },
                    { "8ad463c6-0e8d-4267-9544-e9da8d16db89", 0, "5120da2d-77f8-49ad-8267-22dcdcb02c54", "LocalIdentityUser", "maplewoodhighschool@mail.com", false, false, null, "MAPLEWOODHIGHSCHOOL@MAIL.COM", "MAPLEWOODHIGHSCHOOL", "AQAAAAIAAYagAAAAEEYC43sOhlFZ32W0XOrJ5FiR8wKMmPdGtRv7o/ybUeoTHCrLY/Gzdf4b2YnB+Z5yFA==", null, false, "136bf094-5c02-4cb4-a2ac-87d69da41fc1", false, 6L, "maplewoodhighschool" },
                    { "a95cf4f2-f5d4-4e60-b107-903642c53ef1", 0, "32f8f578-2af0-4554-8351-2b51c05bbf1f", "LocalIdentityUser", "programmingGuru@mail.com", false, false, null, "PROGRAMMINGGURU@MAIL.COM", "PROGRAMMINGGURU", "AQAAAAIAAYagAAAAEBmENgxlPVO/rU6PA0hxx/wW5w3p8Aa/U3X0lsq9/FjblCmKCwo4mbHItzTdcCm08A==", null, false, "dd92c330-6b54-4e8f-af5d-481d0beaca98", false, 11L, "programmingGuru" },
                    { "afe86238-8e5e-4f0e-841f-e47dec7f6c24", 0, "81231057-98c7-4e91-bd70-12a135b801c3", "LocalIdentityUser", "john_the_ipper@mail.com", false, false, null, "JOHN_THE_IPPER@MAIL.COM", "JOHN_THE_IPPER", "AQAAAAIAAYagAAAAEFBAxWw6hE4aTjWBmeIMwACJcsW4FCmTGHTpiyRrrNEpkeE0RUQeCReXstbiV7qrBA==", null, false, "22ec7d83-a170-4a37-a09a-6774cacab29e", false, 15L, "john_the_ipper" },
                    { "bff12893-18ee-49f1-a063-b7a4c722b265", 0, "df7947f2-e4c8-4e64-8188-1ca29a45a797", "LocalIdentityUser", "foodLover88@mail.com", false, false, null, "FOODLOVER88@MAIL.COM", "FOODLOVER88", "AQAAAAIAAYagAAAAEJmEmdd6sIYaHGZeo6Yy2c9fypXMViunqCQu8CNFH6WTQXfX6FD3Y/1wTIbAMf/GkA==", null, false, "a252ac23-9775-4528-bd4f-a9dea2838a5c", false, 14L, "foodLover88" },
                    { "c09fa3c9-7ddf-4c0b-9db9-1b3ccde2922d", 0, "ea98d5cf-2ddd-41c2-8568-dce6c77225d1", "LocalIdentityUser", "emily_in_paris@mail.com", false, false, null, "EMILY_IN_PARIS@MAIL.COM", "EMILY_IN_PARIS", "AQAAAAIAAYagAAAAEGPYE3O5a54FKF7oISCKim6ouJ7BK9D959ZzajHApqossaAYulqYyOEFnXKRyLmC7Q==", null, false, "ac051f08-71f4-4e3a-abbe-1400a4308463", false, 4L, "emily_in_paris" },
                    { "c4f2e8a8-3879-4c64-afd2-465396218f5e", 0, "e9824871-0970-42a8-9ffe-ba6ccb221f16", "LocalIdentityUser", "Housemaster111@mail.com", false, false, null, "HOUSEMASTER111@MAIL.COM", "HOUSEMASTER111", "AQAAAAIAAYagAAAAENrN2vhrJNzq8M84BzWsDOK25WRagmr5gT7jUSDl8X7Ae8qsQlHFTddDE3rhkA6fyQ==", null, false, "5634a3ba-4f2b-4ed8-9010-bcea35cba9d7", false, 1L, "Housemaster111" },
                    { "f7f74bdf-fbd2-4896-8d1f-972233e5c4f5", 0, "c987af43-49b1-4eb1-9b70-4998465b91a9", "LocalIdentityUser", "22avidReader22@mail.com", false, false, null, "22AVIDREADER22@MAIL.COM", "22AVIDREADER22", "AQAAAAIAAYagAAAAENf7yQ5zki84Llly0v6OSHU6ITg0BhsjFRrIc19X8Y0P/9gs0pg1xViwLYTIVnLG/A==", null, false, "ace5807e-0134-47fb-adb9-665929249375", false, 10L, "22avidReader22" },
                    { "f8ae106d-5578-4b47-b16d-12da1153d0be", 0, "901a0c92-7357-410c-b713-0fce4a249bd2", "LocalIdentityUser", "booklover88@mail.com", false, false, null, "BOOKLOVER88@MAIL.COM", "BOOKLOVER88", "AQAAAAIAAYagAAAAEPb+iSYscLQZhOm5H9rtsvz3+bS+OMttxZhoP7xMmAvhxx4EuD19yI4VPII6EXqD0w==", null, false, "aa77d154-7eff-419b-8d49-d62d0377af6b", false, 5L, "booklover88" }
                });

            migrationBuilder.InsertData(
                table: "BookStores",
                columns: new[] { "Id", "AddressId", "Email", "ManagerId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1L, 1L, "bestBookstore@gmail.com", 1L, "Brno Michelle Bookstore", "+421 918 365 172" },
                    { 2L, 3L, "cityreads@example.com", 2L, "City Reads", "+123 456 7890" },
                    { 3L, 4L, "bookhaven@store.net", 3L, "Book Haven", "+987 654 3210" },
                    { 4L, 5L, "nook.reads@bookshop.org", 4L, "Reading Nook", "+555 123 4567" },
                    { 5L, 6L, "litlighthouse@mail.com", 5L, "Literary Lighthouse", "+777 999 8888" },
                    { 6L, 7L, "pageturner@store.info", 6L, "Page Turner Books", "+333 555 7777" },
                    { 7L, 8L, "classicreads@example.com", 7L, "Classic Reads", "+999 111 2222" },
                    { 8L, 9L, "emporium@bookstore.net", 8L, "Book Emporium", "+123 987 6543" },
                    { 9L, 10L, "novelnook@books.biz", 9L, "Novel Nook", "+555 777 3333" },
                    { 10L, 11L, "pagemaster@example.com", 10L, "PageMaster Books", "+666 888 2222" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "BookGenre", "Description", "ISBN", "Price", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1L, 28, "Have you always wanted to learn computer programming but are afraid it'll be too difficult for you? Or perhaps you know other programming languages but are interested in learning the C# language fast? This book is for you. You no longer have to waste your time and money learning C# from boring books that are 600 pages long, expensive online courses or complicated C# tutorials that just leave you more confused.", "978-1518800276", 10.58, 1L, "Learn C# in One Day and Learn It Well" },
                    { 2L, 28, "Creating top-notch software is an extremely difficult undertaking. Developers researching the subject have difficulty determining which advice is up to date and which approaches have already been replaced by easier, better practices. At the same time, most online resources offer limited explanation, while also lacking the proper context and structure.", "978-1801070058", 35.990000000000002, 2L, "Modern CMake for C++: Discover a better approach to building, testing, and packaging your software" },
                    { 3L, 5, "Harry Potter has no idea how famous he is. That's because he's being raised by his miserable aunt and uncle who are terrified Harry will learn that he's really a wizard, just as his parents were. But everything changes when Harry is summoned to attend an infamous school for wizards, and he begins to discover some clues about his illustrious birthright. From the surprising way he is greeted by a lovable giant, to the unique curriculum and colorful faculty at his unusual school, Harry finds himself drawn deep inside a mystical world he never knew existed and closer to his own noble destiny.", "978-1338878929", 6.7999999999999998, 3L, "Harry Potter and the Sorcerer's Stone" },
                    { 4L, 5, "The Dursleys were so mean and hideous that summer that all Harry Potter wanted was to get back to the Hogwarts School for Witchcraft and Wizardry. But just as he's packing his bags, Harry receives a warning from a strange, impish creature named Dobby who says that if Harry Potter returns to Hogwarts, disaster will strike. And strike it does. For in Harry's second year at Hogwarts, fresh torments and horrors arise, including an outrageously stuck-up new professor, Gilderoy Lockhart, a spirit named Moaning Myrtle who haunts the girls' bathroom, and the unwanted attentions of Ron Weasley's younger sister, Ginny.", "978-1338878936", 6.2999999999999998, 3L, "Harry Potter and the Chamber of Secrets" },
                    { 5L, 5, "For twelve long years, the dread fortress of Azkaban held an infamous prisoner named Sirius Black. Convicted of killing thirteen people with a single curse, he was said to be the heir apparent to the Dark Lord, Voldemort.Now he has escaped, leaving only two clues as to where he might be headed: Harry Potter's defeat of You-Know-Who was Black's downfall as well. And the Azkaban guards heard Black muttering in his sleep, \"He's at Hogwarts... he's at Hogwarts.\"Harry Potter isn't safe, not even within the walls of his magical school, surrounded by his friends. Because on top of it all, there may be a traitor in their midst.", "978-1338299168", 8.1999999999999993, 3L, "Harry Potter and the Prisoner of Azkaban" },
                    { 6L, 5, "Harry wants to get away from the pernicious Dursleys and go to the International Quidditch Cup with Hermione, Ron, and the Weasleys. He wants to dream about Cho Chang, his crush (and maybe do more than dream). He wants to find out about the mysterious event involving two other rival schools of magic, and a competition that hasn't happened for a hundred years. He wants to be a normal, fourteen-year-old wizard. Unfortunately for Harry Potter, he's not normal - even by wizarding standards.And in this case, different can be deadly.", "978-1338878950", 11.99, 3L, "Harry Potter and the Goblet of Fire" },
                    { 7L, 5, "There is a door at the end of a silent corridor. And it's haunting Harry Potter's dreams. Why else would he be waking in the middle of the night, screaming in terror?It's not just the upcoming O.W.L. exams; a new teacher with a personality like poisoned honey; a venomous, disgruntled house-elf; or even the growing threat of He-Who-Must-Not-Be-Named. Now Harry Potter is faced with the unreliability of the very government of the magical world and the impotence of the authorities at Hogwarts.Despite this (or perhaps because of it), he finds depth and strength in his friends, beyond what even he knew; boundless loyalty; and unbearable sacrifice.", "978-1338299182", 9.3000000000000007, 3L, "Harry Potter and the Order of the Phoenix" },
                    { 8L, 5, "The war against Voldemort is not going well; even Muggle governments are noticing. Ron scans the obituary pages of the Daily Prophet, looking for familiar names. Dumbledore is absent from Hogwarts for long stretches of time, and the Order of the Phoenix has already suffered losses. And yet... As in all wars, life goes on. Sixth-year students learn to Apparate - and lose a few eyebrows in the process. The Weasley twins expand their business. Teenagers flirt and fight and fall in love. Classes are never straightforward, though Harry receives some extraordinary help from the mysterious Half-Blood Prince.", "978-1338878974", 9.3499999999999996, 3L, "Harry Potter and the Half-Blood Prince" },
                    { 9L, 5, "Will Harry die in the final battle against the mighty Voldemort? Maybe. Just read the book and you will find out.", "978-1408855713", 8.9499999999999993, 3L, "Harry Potter and the Deathly Hallows" },
                    { 10L, 2, "Gripping mystery novel that delves into a world of secrets, where a seemingly ordinary door conceals a web of intrigue, unsolved crimes, and dark truths. As the detective Mike unravels the mysteries lurking \"behind the real door,\" the line between reality and illusion blurs, keeping readers on the edge of their seats until the shocking climax.", "121-1409055700", 120.2, 4L, "Behind the real door" },
                    { 11L, 2, "In 'Whispers in the Shadows,' a small, enigmatic town hides dark secrets. As a series of eerie whispers and unexplained phenomena plague its residents, a determined investigator Mike must unearth the truth, facing a tangled web of deceit, paranormal occurrences, and a past that refuses to stay buried. With every page, the mystery deepens, keeping readers spellbound.", "121-1409055701", 119.98999999999999, 4L, "Whispers in the Shadows" },
                    { 12L, 36, "Matej K., one of the greatest programmers and mathematicians of all time is sharing some of his knowledge with the readers. In his endles list of successes he helped Elon Musk build the ship to Mars, ended the world hunger and created a completly new approach to AI. Join him in this journey accross universes and take a glimpse look into his life full of interesting events.", "420-4204204200", 30.989999999999998, 5L, "Memoirs of the Matej K., the great one" },
                    { 13L, 13, "Discover a world of gastronomic pleasures in 'Culinary Delights.' This recipe book takes you on a mouthwatering journey through diverse cuisines, offering a delightful array of dishes from appetizers to desserts. Whether you're a novice cook or a seasoned chef, you'll find inspiration and easy-to-follow recipes that will tantalize your taste buds and elevate your culinary skills.", "321-7503791824", 15.0, 6L, "Culinary Delights" },
                    { 14L, 53, "Hi, my name is Mark, the CEO of Meta. Eons ago I created a funny app for sharing called facebook. In this book I will present to you, my audience (or simply my subjects), how we, the lizzardmen, are sharing data. This guide is mainly for children.", "816-0815794691", 0.5, 7L, "How to teach kids to share" },
                    { 15L, 11, "From the periodic table to chemical reactions, this book demystifies the complexities of chemistry. Engaging explanations and real-world applications make it a captivating journey through the science that shapes our daily lives, from the laboratory to the natural world.", "936-7213567800", 9.9900000000000002, 8L, "Elemental: How the Periodic Table Can Now Explain (Nearly) Everything" },
                    { 16L, 11, "Join Sarah on a breathtaking adventure to unravel the mysteries of the skies. \"Eternal Skies\" explores meteorology and the wonders of the atmosphere, revealing the secrets hidden in every cloud and the magic of the ever-changing weather. A perfect guide for aspiring meteorologists and weather enthusiasts.", "978-1234567890", 24.989999999999998, 9L, "Eternal Skies" },
                    { 17L, 11, "Dr. Amelia Stanton embarks on a thrilling archaeological quest to decode an ancient scroll. Uncover lost civilizations, hidden treasures, and cryptic messages as you follow her journey through time and space in \"Secrets of the Lost Scroll.\"", "111-2850195739", 18.949999999999999, 10L, "Secrets of the Lost Scroll" },
                    { 18L, 28, "Bjarne Stroustrup provides an overview of ISO C++, C++20, that aims to give experienced programmers a clear understanding of what constitutes modern C++. Featuring carefully crafted examples and practical help in getting started, this revised and updated edition concisely covers most major language features and the major standard-library components needed for effective use.", "978-0136816485", 29.989999999999998, 11L, "Tour of C++" },
                    { 19L, 45, "In 1986, Frank Miller and David Mazzucchelli produced this groundbreaking reinterpretation of the origin of Batman—who he is, and how he came to be. Sometimes careless and naive, this Dark Knight is far from the flawless vigilante he is today.In his first year on the job, Batman feels his way around a Gotham City far darker than the one he left. His solemn vow to extinguish the town’s criminal element is only half the battle; along with Lieutenant James Gordon, the Dark Knight must also fight a police force more corrupt than the scum in the streets.", "978-0290204890", 9.2899999999999991, 12L, "Batman: Year One" },
                    { 20L, 45, "Now Batman must race to stop his archnemesis before his reign of terror claims two of the Dark Knight's closest friends. Can he finally put an end to the cycle of bloodlust and lunacy that links these two iconic foes before it leads to its fatal conclusion? And as the horrifying origin of the Clown Prince of Crime is finally revealed, will the thin line that separates Batman's nobility and the Joker's insanity snap once and for all?", "978-1401294052", 11.19, 12L, "Batman the Killing Joke: The Deluxe Edition" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "State", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(639), 3, 4L },
                    { 2L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(651), 0, 4L },
                    { 3L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(654), 0, 5L },
                    { 4L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(660), 2, 5L },
                    { 5L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(663), 1, 6L },
                    { 6L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(670), 1, 7L },
                    { 7L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(672), 3, 7L },
                    { 8L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(675), 3, 7L },
                    { 9L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(677), 0, 7L },
                    { 10L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(680), 1, 8L },
                    { 11L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(714), 1, 8L },
                    { 12L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(717), 0, 8L },
                    { 13L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(720), 0, 8L },
                    { 14L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(722), 3, 8L },
                    { 15L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(725), 1, 15L },
                    { 16L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(727), 1, 15L }
                });

            migrationBuilder.InsertData(
                table: "WishList",
                columns: new[] { "Id", "CreatedAt", "Description", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(529), "I'd love to add 'Learn C# in One Day and Learn It Well' by Jamie Chan to my collection. It seems like a concise guide to quickly grasp the concepts of C#.", 1L },
                    { 2L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(609), "The 'Modern CMake for C++' book by Rafal Swidzinski has caught my attention. I've heard it offers a fresh perspective on building and packaging software efficiently.", 2L },
                    { 3L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(612), "I've been thoroughly enjoying the Harry Potter series. Next on my list are 'Harry Potter and the Chamber of Secrets', 'Harry Potter and the Prisoner of Azkaban', and 'Harry Potter and the Goblet of Fire'. Each one promises more exciting adventures and mysteries at Hogwarts. Can't wait to dive into them!", 3L },
                    { 4L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(614), "Moving away from fantasy, the mystery novel 'Behind the real door' by Jack Sparknotes has been suggested to me. The concept of secrets behind a door sounds like a thrilling read!", 4L },
                    { 5L, new DateTime(2024, 1, 14, 22, 30, 4, 200, DateTimeKind.Local).AddTicks(616), "I'm eager to delve deeper into Batman's lore. 'Batman: Year One' by Frank Miller sounds captivating with its raw and gritty reinterpretation of Batman's origin. I'm also intrigued by 'Batman the Killing Joke: The Deluxe Edition' by Alan Moore. The intense rivalry and the blurred line between Batman and Joker have always fascinated me. Both these masterpieces are must-haves for my collection.", 5L }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ad91e7c3-e544-4087-a661-0e5af1e69bc3", "0afb91aa-9fb2-4171-ab18-9f237a7a0501" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "16f0fb39-cbd0-4264-900f-e94f57c07f20" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "38af2f22-852a-4277-aff1-dac5ed58bc8b" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", "519b8c84-be71-44bb-9ebc-ff3e327aa2d5" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "51a24170-f3a4-4137-9b8f-e1232a62f925" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", "5c88336b-8744-4bfa-b741-f0adaa0bddc3" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "67a545ea-5107-456e-8cf6-93a3d9f10612" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "6edc1bd4-676f-4f2e-98c5-e1f777bf1917" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", "76cab806-9f8e-4956-83d2-dbde8f43b39b" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "8ad463c6-0e8d-4267-9544-e9da8d16db89" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", "a95cf4f2-f5d4-4e60-b107-903642c53ef1" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", "afe86238-8e5e-4f0e-841f-e47dec7f6c24" },
                    { "d4e24154-7f04-4ab4-a903-7f1697e34df4", "bff12893-18ee-49f1-a063-b7a4c722b265" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "c09fa3c9-7ddf-4c0b-9db9-1b3ccde2922d" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "c4f2e8a8-3879-4c64-afd2-465396218f5e" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "f7f74bdf-fbd2-4896-8d1f-972233e5c4f5" },
                    { "6f6375de-9a1d-4cd6-8218-ae7c55d2c838", "f8ae106d-5578-4b47-b16d-12da1153d0be" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBookAssociations",
                columns: new[] { "Id", "AuthorId", "BookId", "IsPrimary" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, true },
                    { 2L, 2L, 1L, false },
                    { 3L, 3L, 2L, true },
                    { 4L, 4L, 3L, true },
                    { 5L, 4L, 4L, true },
                    { 6L, 4L, 5L, true },
                    { 7L, 4L, 6L, true },
                    { 8L, 4L, 7L, true },
                    { 9L, 4L, 8L, true },
                    { 10L, 4L, 9L, true },
                    { 11L, 5L, 10L, true },
                    { 12L, 5L, 11L, true },
                    { 13L, 6L, 12L, true },
                    { 14L, 7L, 13L, true },
                    { 15L, 8L, 14L, true },
                    { 16L, 9L, 15L, true },
                    { 17L, 10L, 17L, true },
                    { 18L, 11L, 18L, true },
                    { 19L, 12L, 19L, true },
                    { 20L, 13L, 20L, true }
                });

            migrationBuilder.InsertData(
                table: "BookReviews",
                columns: new[] { "Id", "BookId", "Description", "Rating", "ReviewerId" },
                values: new object[,]
                {
                    { 1L, 1L, "A great book to start learning C#. It covers all the fundamentals and provides clear explanations. Highly recommended for beginners.", 5, 3L },
                    { 2L, 2L, "This book on modern CMake is a game-changer. It's well-structured and makes complex concepts easy to understand. A must-read for C++ developers.", 5, 5L },
                    { 3L, 3L, "Harry Potter is a classic, and this book is where the magical journey begins. It's a must-read for fantasy lovers of all ages.", 4, 4L },
                    { 4L, 4L, "The second book in the Harry Potter series continues to captivate readers with its magic and mystery. A great follow-up to the first book.", 4, 7L },
                    { 5L, 5L, "The third book in the Harry Potter series introduces new elements to the story, making it even more exciting. J.K. Rowling's writing is exceptional.", 5, 6L },
                    { 6L, 6L, "Another fantastic addition to the Harry Potter series. The Goblet of Fire is full of surprises and keeps you engaged throughout.", 5, 3L },
                    { 7L, 7L, "The Order of the Phoenix takes the series to a darker place. It's a compelling read with intense moments and character development.", 4, 4L },
                    { 8L, 8L, "The Half-Blood Prince is a pivotal book in the Harry Potter series. It reveals crucial information and sets the stage for the final showdown.", 5, 6L },
                    { 9L, 9L, "A thrilling conclusion to the Harry Potter saga. All loose ends are tied up, and it's an emotional journey for readers.", 5, 7L },
                    { 10L, 10L, "A mysterious door leads to an intriguing plot. 'Behind the Real Door' is a gripping mystery novel that keeps you guessing until the end.", 4, 5L },
                    { 11L, 11L, "I didn't enjoy this book at all. The plot was confusing, and the characters were unrelatable. Would not recommend.", 1, 3L },
                    { 12L, 12L, "I found 'Culinary Delights' to be underwhelming. The recipes were basic, and I was expecting more innovative dishes.", 2, 4L },
                    { 13L, 13L, "This book was a disappointment. The writing style was dry, and the characters lacked depth. Not recommended.", 2, 5L },
                    { 14L, 14L, "I couldn't finish 'How to teach kids to share.' It felt like a poorly written joke. Save your money and time.", 1, 6L },
                    { 15L, 15L, "I expected 'Elemental' to be an exciting exploration of meteorology, but it was overly technical and lacked engaging content.", 2, 7L },
                    { 16L, 16L, "Despite its title, 'Eternal Skies' failed to deliver a captivating narrative about meteorology. It felt dull and uninspiring.", 2, 3L },
                    { 17L, 17L, "I had high hopes for 'Secrets of the Lost Scroll,' but the plot was convoluted, and the characters were forgettable.", 2, 4L },
                    { 18L, 18L, "I expected 'Tour of C++' to be a comprehensive guide, but it felt disjointed and lacked clear explanations. Disappointing.", 2, 5L },
                    { 19L, 19L, "Batman: Year One was not as engaging as I hoped. The storyline was uninspiring, and the artwork didn't impress me.", 2, 6L },
                    { 20L, 20L, "I found 'Batman the Killing Joke' to be too violent and disturbing for my taste. It didn't live up to the hype.", 2, 7L },
                    { 27L, 8L, "I had high expectations for 'Harry Potter and the Half-Blood Prince,' but I found it lacking in depth compared to the previous books in the series.", 3, 8L },
                    { 28L, 9L, "A thrilling conclusion to the Harry Potter series. It answers many questions and provides a satisfying end to the story. Highly recommended.", 3, 9L },
                    { 29L, 10L, "I thoroughly enjoyed 'Behind the Real Door.' The plot was intriguing, and it kept me guessing until the end. A must-read mystery novel.", 4, 10L },
                    { 30L, 18L, "As a C++ developer, I expected more from 'Tour of C++.' It felt incomplete and lacked the depth I needed for advanced programming.", 2, 11L },
                    { 31L, 14L, "I didn't find 'How to teach kids to share' engaging or informative. It failed to deliver practical advice for teaching children about sharing.", 2, 12L },
                    { 32L, 15L, "I was disappointed with 'Elemental.' It promised to simplify chemistry but ended up being overly technical. Not a recommended read.", 2, 13L },
                    { 33L, 11L, "I found 'Whispers in the Shadows' to be a captivating mystery novel. It kept me hooked with its eerie atmosphere and intriguing plot.", 4, 14L }
                });

            migrationBuilder.InsertData(
                table: "InventoryItems",
                columns: new[] { "Id", "BookId", "BookStoreId", "InStock", "LastRestock" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 2L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, 3L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, 4L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5L, 5L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6L, 6L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7L, 7L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8L, 8L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9L, 9L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10L, 10L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11L, 11L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12L, 12L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13L, 13L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14L, 14L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15L, 15L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16L, 16L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17L, 17L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18L, 18L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19L, 19L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20L, 20L, 1L, 5u, new DateTime(2023, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21L, 1L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22L, 2L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23L, 3L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24L, 4L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25L, 5L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26L, 6L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27L, 7L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28L, 8L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29L, 9L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30L, 10L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31L, 11L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32L, 12L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33L, 13L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34L, 14L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35L, 15L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36L, 16L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37L, 17L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38L, 18L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39L, 19L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40L, 20L, 2L, 5u, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41L, 1L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42L, 2L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43L, 3L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44L, 4L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45L, 5L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46L, 6L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47L, 7L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48L, 8L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49L, 9L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50L, 10L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51L, 11L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52L, 12L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53L, 13L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54L, 14L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55L, 15L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56L, 16L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57L, 17L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58L, 18L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59L, 19L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60L, 20L, 3L, 2u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61L, 1L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62L, 2L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63L, 3L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64L, 4L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65L, 5L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66L, 6L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67L, 7L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68L, 8L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69L, 9L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70L, 10L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71L, 11L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72L, 12L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73L, 13L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74L, 14L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75L, 15L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76L, 16L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77L, 17L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78L, 18L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79L, 19L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80L, 20L, 4L, 7u, new DateTime(2023, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81L, 1L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82L, 2L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83L, 3L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84L, 4L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85L, 5L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86L, 6L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87L, 7L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88L, 8L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89L, 9L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90L, 10L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91L, 11L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92L, 12L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93L, 13L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94L, 14L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95L, 15L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96L, 16L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97L, 17L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98L, 18L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99L, 19L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100L, 20L, 5L, 10u, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "BookId", "BookStoreId", "OrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1L, 3L, 2L, 2L, 6.7999999999999998, 1u },
                    { 2L, 3L, 1L, 3L, 6.7999999999999998, 1u },
                    { 3L, 4L, 1L, 3L, 6.2999999999999998, 1u },
                    { 4L, 6L, 1L, 4L, 11.99, 1u },
                    { 5L, 20L, 3L, 5L, 111.90000000000001, 10u },
                    { 6L, 3L, 1L, 6L, 6.7999999999999998, 1u },
                    { 7L, 4L, 1L, 6L, 6.2999999999999998, 1u },
                    { 8L, 5L, 1L, 6L, 8.1999999999999993, 1u },
                    { 9L, 6L, 1L, 6L, 11.9, 1u },
                    { 10L, 7L, 1L, 6L, 9.3000000000000007, 1u },
                    { 11L, 8L, 1L, 6L, 9.3499999999999996, 1u },
                    { 12L, 9L, 1L, 6L, 8.9499999999999993, 1u },
                    { 13L, 1L, 2L, 10L, 105.8, 10u },
                    { 14L, 2L, 2L, 10L, 350.89999999999998, 10u },
                    { 15L, 18L, 2L, 10L, 290.89999999999998, 10u },
                    { 16L, 19L, 3L, 11L, 46.450000000000003, 5u },
                    { 17L, 20L, 3L, 11L, 33.57, 3u },
                    { 18L, 12L, 3L, 12L, 30.989999999999998, 1u },
                    { 19L, 13L, 3L, 12L, 15.0, 1u },
                    { 20L, 10L, 1L, 14L, 120.09999999999999, 1u },
                    { 21L, 11L, 1L, 14L, 119.98999999999999, 1u },
                    { 22L, 14L, 1L, 15L, 0.5, 1u },
                    { 23L, 16L, 2L, 16L, 24.989999999999998, 1u }
                });

            migrationBuilder.InsertData(
                table: "WishListItem",
                columns: new[] { "Id", "BookId", "PreferencePriority", "WishListId" },
                values: new object[,]
                {
                    { 1L, 1L, 1u, 1L },
                    { 2L, 2L, 1u, 2L },
                    { 3L, 18L, 2u, 2L },
                    { 4L, 3L, 1u, 3L },
                    { 5L, 4L, 2u, 3L },
                    { 6L, 5L, 0u, 3L },
                    { 7L, 6L, 1u, 4L },
                    { 8L, 7L, 1u, 5L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookAssociations_AuthorId",
                table: "AuthorBookAssociations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookAssociations_BookId",
                table: "AuthorBookAssociations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_ReviewerId",
                table: "BookReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookStores_AddressId",
                table: "BookStores",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookStores_ManagerId",
                table: "BookStores",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_BookId",
                table: "InventoryItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_BookStoreId",
                table: "InventoryItems",
                column: "BookStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookStoreId",
                table: "OrderItems",
                column: "BookStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserId",
                table: "WishList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_BookId",
                table: "WishListItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItem_WishListId",
                table: "WishListItem",
                column: "WishListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorBookAssociations");

            migrationBuilder.DropTable(
                name: "BookReviews");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "WishListItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "BookStores");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
