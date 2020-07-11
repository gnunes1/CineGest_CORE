using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class TicketAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Timestamp" },
                values: new object[] { "ca80d526-9f68-4c27-a31c-c5d057a5ef55", "AQAAAAEAACcQAAAAEDdRkTN/QczU5TTleprFcErpyBPpqMkdW4StzcFx8mSrkuuq2Fw/bxbx+nnd5ykzEw==", new DateTime(2020, 7, 10, 16, 37, 24, 882, DateTimeKind.Local).AddTicks(303) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 7, 10, 15, 37, 24, 870, DateTimeKind.Utc).AddTicks(5807));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
