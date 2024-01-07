using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AuthorPublisherMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Books");

            migrationBuilder.AddColumn<long>(
                name: "PublisherId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false)
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
                    Name = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBookAssociations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorId = table.Column<long>(type: "INTEGER", nullable: false),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Jamie Chan" },
                    { 2L, "Jackie Chan" },
                    { 3L, "Rafal Swidzinski" },
                    { 4L, "J.K. Rowling" },
                    { 5L, "Jack Sparknotes" },
                    { 6L, "Matej K." },
                    { 7L, "K. Racer" },
                    { 8L, "Mark Zuckerberg" },
                    { 9L, "Heisenberg" },
                    { 10L, "James R. Anderson" },
                    { 11L, "Bjarne Stroustrup" },
                    { 12L, "Frank Miller" },
                    { 13L, "Alan Moore" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1L,
                column: "PublisherId",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PublisherId",
                value: 2L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9L,
                column: "PublisherId",
                value: 3L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10L,
                column: "PublisherId",
                value: 4L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11L,
                column: "PublisherId",
                value: 4L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12L,
                column: "PublisherId",
                value: 5L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13L,
                column: "PublisherId",
                value: 6L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14L,
                column: "PublisherId",
                value: 7L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15L,
                column: "PublisherId",
                value: 8L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16L,
                column: "PublisherId",
                value: 9L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17L,
                column: "PublisherId",
                value: 10L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18L,
                column: "PublisherId",
                value: 11L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19L,
                column: "PublisherId",
                value: 12L);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20L,
                column: "PublisherId",
                value: 12L);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6443));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6449));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6451));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6453));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6455));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6457));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6460));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6462));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6464));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6466));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6468));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6470));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6472));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6476));

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "CreateSpace Independent Publishing Platform" },
                    { 2L, "Packt Publishing" },
                    { 3L, "Scholastic" },
                    { 4L, "Next door Publishing" },
                    { 5L, "Matej K." },
                    { 6L, "World Wide Publishing" },
                    { 7L, "Facebook Publishing Company" },
                    { 8L, "Hachette UK" },
                    { 9L, "Horizon Publications" },
                    { 10L, "Enigma Press" },
                    { 11L, "Addison-Wesley Professional" },
                    { 12L, "DC Comics" }
                });

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6363));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 14, 19, 37, 25, 562, DateTimeKind.Local).AddTicks(6426));

            migrationBuilder.InsertData(
                table: "AuthorBookAssociations",
                columns: new[] { "Id", "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 2L, 1L },
                    { 3L, 3L, 2L },
                    { 4L, 4L, 3L },
                    { 5L, 4L, 4L },
                    { 6L, 4L, 5L },
                    { 7L, 4L, 6L },
                    { 8L, 4L, 7L },
                    { 9L, 4L, 8L },
                    { 10L, 4L, 9L },
                    { 11L, 5L, 10L },
                    { 12L, 5L, 11L },
                    { 13L, 6L, 12L },
                    { 14L, 7L, 13L },
                    { 15L, 8L, 14L },
                    { 16L, 9L, 15L },
                    { 17L, 10L, 17L },
                    { 18L, 11L, 18L },
                    { 19L, 12L, 19L },
                    { 20L, 13L, 20L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookAssociations_AuthorId",
                table: "AuthorBookAssociations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBookAssociations_BookId",
                table: "AuthorBookAssociations",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "AuthorBookAssociations");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Books_PublisherId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Jamie Chan", "CreateSpace Independent Publishing Platform" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Rafal Swidzinski", "Packt Publishing" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "J.K. Rowling", "Scholastic" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Jack Sparknotes", "Next door Publishing" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Jack Sparknotes", "Next door Publishing" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Matej K.", "Matej K." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "K. Racer", "World Wide Publishing" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Mark Zuckerberg", "Facebook Publishing Company" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Heisenberg", "Hachette UK" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Samantha Mitchell", "Horizon Publications" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "James R. Anderson", "Enigma Press" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Bjarne Stroustrup", "Addison-Wesley Professional" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Frank Miller", "DC Comics" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "Author", "Publisher" },
                values: new object[] { "Alan Moore", "DC Comics" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1305));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1308));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1310));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1315));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1317));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1319));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1321));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1323));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1326));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1328));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1330));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1332));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1334));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1337));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1339));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1231));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1283));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1285));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1287));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 29, 20, 11, 17, 913, DateTimeKind.Local).AddTicks(1289));
        }
    }
}
