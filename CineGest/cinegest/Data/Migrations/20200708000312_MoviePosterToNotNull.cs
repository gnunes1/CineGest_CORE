using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class MoviePosterToNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Poster",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "629e8d27-9a03-42a0-9ee3-3ed889958c30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d69df0f7-9f29-4ae9-a9b9-f46a16c764a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Timestamp" },
                values: new object[] { "9b60e6c4-0ad5-4e00-92b4-c01f4b192e5e", "AQAAAAEAACcQAAAAEH5y6MHmbHFSBJ1HXoW9zF65OnGfDeqCXRLN7KZRqEdkD2P3hahtU3NybdXIVPPxuA==", new DateTime(2020, 7, 8, 1, 3, 12, 51, DateTimeKind.Local).AddTicks(8607) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 7, 8, 0, 3, 12, 37, DateTimeKind.Utc).AddTicks(110));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Poster",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f067d9c4-01c0-4dbb-ab20-79ae78754ed1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1ab6f89b-8af3-48cc-9dbd-8f96a7fa0d44");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Timestamp" },
                values: new object[] { "6797fc61-2f4a-4a8c-8723-3188a0af01de", "AQAAAAEAACcQAAAAEGMSIXigmxvZN/CtOIKWCkgrh7Zxzcxbf4yddkwKvgaynX4jhFV6dqPd8M3TnrALZw==", new DateTime(2020, 6, 30, 20, 50, 56, 756, DateTimeKind.Local).AddTicks(476) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 30, 19, 50, 56, 743, DateTimeKind.Utc).AddTicks(9157));
        }
    }
}
