using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryAuthorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "AuthorBookAssociations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 1L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 2L,
                column: "IsPrimary",
                value: false);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 3L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 4L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 5L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 6L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 7L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 8L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 9L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 10L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 11L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 12L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 13L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 14L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 15L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 16L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 17L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 18L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 19L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "AuthorBookAssociations",
                keyColumn: "Id",
                keyValue: 20L,
                column: "IsPrimary",
                value: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1996));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1999));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2001));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2003));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2008));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2010));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2012));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2017));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2019));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2021));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2023));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2025));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2027));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(2029));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1932));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1975));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1977));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1979));

            migrationBuilder.UpdateData(
                table: "WishList",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 7, 14, 46, 12, 445, DateTimeKind.Local).AddTicks(1981));

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "AuthorBookAssociations");

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
        }
    }
}
