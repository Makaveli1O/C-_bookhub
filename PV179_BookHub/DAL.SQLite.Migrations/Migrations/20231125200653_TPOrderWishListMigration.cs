using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class TPOrderWishListMigration : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_BookStores_BookStoreId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BookStores_BookStoreId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Books_BookId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PreferencePriorty",
                table: "WishListItem",
                newName: "PreferencePriority");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8701));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8706));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8708));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8710));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8714));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8717));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8718));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8721));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8723));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8725));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8727));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8729));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8731));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8733));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8735));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "YearFounded",
                value: (ushort)2008);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "YearFounded",
                value: (ushort)2003);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "YearFounded",
                value: (ushort)1995);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "YearFounded",
                value: (ushort)2022);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "YearFounded",
                value: (ushort)2020);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "YearFounded",
                value: (ushort)2005);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 7L,
                column: "YearFounded",
                value: (ushort)890);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 8L,
                column: "YearFounded",
                value: (ushort)1989);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 9L,
                column: "YearFounded",
                value: (ushort)2013);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 10L,
                column: "YearFounded",
                value: (ushort)1942);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 11L,
                column: "YearFounded",
                value: (ushort)2018);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 12L,
                column: "YearFounded",
                value: (ushort)2010);

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8626));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8678));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 25, 21, 6, 53, 60, DateTimeKind.Local).AddTicks(8685));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_BookStores_BookStoreId",
                table: "InventoryItems",
                column: "BookStoreId",
                principalTable: "BookStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BookStores_BookStoreId",
                table: "OrderItems",
                column: "BookStoreId",
                principalTable: "BookStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Books_BookId",
                table: "OrderItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_BookStores_BookStoreId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BookStores_BookStoreId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Books_BookId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "PreferencePriority",
                table: "WishListItem",
                newName: "PreferencePriorty");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9444), 0.0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9448), 6.7999999999999998 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9451), 13.1 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9453), 11.99 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9455), 111.90000000000001 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9458), 60.799999999999997 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9460), 0.0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9462), 0.0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9464), 0.0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9467), 747.60000000000002 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9469), 80.019999999999996 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9471), 45.990000000000002 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9473), 0.0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9475), 240.09 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9477), 0.5 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "TotalPrice" },
                values: new object[] { new DateTime(2023, 11, 18, 20, 59, 15, 439, DateTimeKind.Local).AddTicks(9480), 24.989999999999998 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "YearFounded",
                value: 2008);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "YearFounded",
                value: 2003);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "YearFounded",
                value: 1995);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "YearFounded",
                value: 2022);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "YearFounded",
                value: 2020);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "YearFounded",
                value: 2005);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 7L,
                column: "YearFounded",
                value: 890);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 8L,
                column: "YearFounded",
                value: 1989);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 9L,
                column: "YearFounded",
                value: 2013);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 10L,
                column: "YearFounded",
                value: 1942);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 11L,
                column: "YearFounded",
                value: 2018);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 12L,
                column: "YearFounded",
                value: 2010);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_BookStores_BookStoreId",
                table: "InventoryItems",
                column: "BookStoreId",
                principalTable: "BookStores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BookStores_BookStoreId",
                table: "OrderItems",
                column: "BookStoreId",
                principalTable: "BookStores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Books_BookId",
                table: "OrderItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
