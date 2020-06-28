using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class AdminSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "1b410465-41b5-41a5-bd93-634bff1247ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "bf20de54-f42d-4e64-b439-f2f1c9993e24");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedUserName", "PasswordHash", "Timestamp", "UserName" },
                values: new object[] { "e3b7b4f8-bc78-4cfb-9722-02022b337c30", null, "ADMIN@ADMIN", "AQAAAAEAACcQAAAAEHLXjT5twwToCfatKGgqHVJ0ni5Im06U2OVVbAm27YMxlvHX6rUTPlpTZYLKtG4BdQ==", new DateTime(2020, 6, 28, 0, 59, 9, 644, DateTimeKind.Local).AddTicks(7254), "admin@admin" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 27, 23, 59, 9, 633, DateTimeKind.Utc).AddTicks(4994));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bcc66d8d-3a74-45ed-af51-902426a4a656");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "95c9a213-fc38-4f87-aca8-ea7236f8145a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedUserName", "PasswordHash", "Timestamp", "UserName" },
                values: new object[] { "ad6e2527-4fb7-410c-85ff-f1419fbe7e1c", "admin@admin", "ADMIN", "AQAAAAEAACcQAAAAEEBMfqTFyMhBbtrVunCJxdRbVzWpujf+SpGn/7B6yUCP06NhScaB/DXgYWsRBLCdZw==", new DateTime(2020, 6, 28, 0, 52, 59, 345, DateTimeKind.Local).AddTicks(7143), "Admin" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 27, 23, 52, 59, 333, DateTimeKind.Utc).AddTicks(6912));
        }
    }
}
