using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class DbContextAddressFixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStores_Users_ManagerId",
                table: "BookStores");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2282));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2284));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2285));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2287));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2289));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2291));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2293));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2294));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2296));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2298));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2299));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2301));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2303));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2304));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2306));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2209));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2260));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2262));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2023, 11, 26, 11, 30, 45, 161, DateTimeKind.Local).AddTicks(2265));

            migrationBuilder.AddForeignKey(
                name: "FK_BookStores_Users_ManagerId",
                table: "BookStores",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStores_Users_ManagerId",
                table: "BookStores");

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
                name: "FK_BookStores_Users_ManagerId",
                table: "BookStores",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
