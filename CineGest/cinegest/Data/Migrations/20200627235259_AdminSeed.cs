using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class AdminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "bcc66d8d-3a74-45ed-af51-902426a4a656", "Admin", "ADMIN" },
                    { "2", "95c9a213-fc38-4f87-aca8-ea7236f8145a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Timestamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "ad6e2527-4fb7-410c-85ff-f1419fbe7e1c", "admin@admin", true, false, null, "Admin", "ADMIN@ADMIN", "ADMIN", "AQAAAAEAACcQAAAAEEBMfqTFyMhBbtrVunCJxdRbVzWpujf+SpGn/7B6yUCP06NhScaB/DXgYWsRBLCdZw==", null, false, "", new DateTime(2020, 6, 28, 0, 52, 59, 345, DateTimeKind.Local).AddTicks(7143), false, "Admin" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 27, 23, 52, 59, 333, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 27, 0, 7, 46, 55, DateTimeKind.Utc).AddTicks(7028));
        }
    }
}
