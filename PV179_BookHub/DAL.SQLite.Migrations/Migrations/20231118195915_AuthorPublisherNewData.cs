using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AuthorPublisherNewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookAssociations_Authors_AuthorId",
                table: "AuthorBookAssociations");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookAssociations_Books_BookId",
                table: "AuthorBookAssociations");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Publishers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Publishers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearFounded",
                table: "Publishers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Authors",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Biography",
                value: "Get to know me, pal");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Biography",
                value: "Kung-fu movie legend from China");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Biography",
                value: "Once upon a time I was dreaming of being a book author");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Biography",
                value: "Enchanting readers with magical worlds in Harry Potter series, a testament to creativity and resilience");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Biography",
                value: "Pioneer of science fiction, exploring robotics and future societies through visionary storytelling");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Biography",
                value: "The great one");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Biography",
                value: "Poetic voice of resilience and hope, inspiring through eloquence and empowerment");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Biography",
                value: "Born as a man that fights with lizzard powers");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Biography",
                value: "Say my name!");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Biography",
                value: "Himsical wordsmith, sparking imagination with playful rhymes and unforgettable characters");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Biography",
                value: "Master of horror, crafting chilling tales that delve into the human psyche and fears");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Biography",
                value: null);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Biography",
                value: null);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9444));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9453));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9455));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9458));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9460));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9462));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9467));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9471));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9473));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9475));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9477));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9480));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "London", "United Kingdom", 2008 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "London", "United Kingdom", 2003 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "New York", "United States", 1995 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { null, "France", 2022 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "Nove Zamky", "Slovakia", 2020 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "Los Angeles", "United States", 2005 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "Mark", "Zuckerbergland", 890 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { null, null, 1989 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "Tokyo", "Japan", 2013 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "Berlin", "Germany", 1942 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { null, "Ireland", 2018 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "City", "Country", "YearFounded" },
                values: new object[] { "Las Vegas", "United States", 2010 });

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9374));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9422));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9424));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9426));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9428));

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookAssociations_Authors_AuthorId",
                table: "AuthorBookAssociations",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookAssociations_Books_BookId",
                table: "AuthorBookAssociations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookAssociations_Authors_AuthorId",
                table: "AuthorBookAssociations");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBookAssociations_Books_BookId",
                table: "AuthorBookAssociations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "YearFounded",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Authors");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookAssociations_Authors_AuthorId",
                table: "AuthorBookAssociations",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBookAssociations_Books_BookId",
                table: "AuthorBookAssociations",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
