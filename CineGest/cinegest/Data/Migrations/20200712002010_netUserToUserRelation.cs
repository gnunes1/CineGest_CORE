using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class netUserToUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5c9ec4e6-d3cd-4c74-a7e3-9558d6bb14d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7bf2243b-4436-44d3-b18d-fe1f5be6043d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "Timestamp", "User" },
                values: new object[] { "3c3a2a1e-94af-40d9-93dc-27bd0cbc1cef", "admin@admin", "AQAAAAEAACcQAAAAEOiOtb65TSQ6xy1DrJlw7w/EK28ZPHnxXBVPrBSoQq98NZL+IFBQrH0w13ermHeBqg==", new DateTime(2020, 7, 12, 1, 20, 10, 325, DateTimeKind.Local).AddTicks(4984), 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 7, 12, 1, 20, 10, 310, DateTimeKind.Local).AddTicks(1276));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "096ca76f-462e-4c43-ad3e-92aa95966f32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "0a7d6375-5d56-4715-adbd-73f320f891a7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "Timestamp" },
                values: new object[] { "ca80d526-9f68-4c27-a31c-c5d057a5ef55", null, "AQAAAAEAACcQAAAAEDdRkTN/QczU5TTleprFcErpyBPpqMkdW4StzcFx8mSrkuuq2Fw/bxbx+nnd5ykzEw==", new DateTime(2020, 7, 10, 16, 37, 24, 882, DateTimeKind.Local).AddTicks(303) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApplicationUser", "DoB" },
                values: new object[] { "1", new DateTime(2020, 7, 10, 15, 37, 24, 870, DateTimeKind.Utc).AddTicks(5807) });
        }
    }
}
