using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class FKLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionsId",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionsId",
                table: "Cinemas",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3595b0a7-c482-44c4-8091-796636160190");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d734d133-5a9b-44e9-b23b-0cf1f492f32a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Timestamp" },
                values: new object[] { "a01ac97c-5d48-4944-993e-9d5df094b2a7", "AQAAAAEAACcQAAAAEP9ryyY0MfE7RrBLOSMnNQpJkj/+qlwet4H7MMupLH0f0CHvgd+CFUkvBmfwgGYolQ==", new DateTime(2020, 6, 30, 12, 54, 1, 244, DateTimeKind.Local).AddTicks(9171) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 30, 11, 54, 1, 228, DateTimeKind.Utc).AddTicks(9514));

            migrationBuilder.CreateIndex(
                name: "IX_Movies_SessionsId",
                table: "Movies",
                column: "SessionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_SessionsId",
                table: "Cinemas",
                column: "SessionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Sessions_SessionsId",
                table: "Cinemas",
                column: "SessionsId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Sessions_SessionsId",
                table: "Movies",
                column: "SessionsId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Sessions_SessionsId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Sessions_SessionsId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_SessionsId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_SessionsId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "SessionsId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "SessionsId",
                table: "Cinemas");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "eaf469a3-b694-4f42-814c-72dc7d50ce70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "84191db0-eaba-41cf-b473-153a3de5ce1e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Timestamp" },
                values: new object[] { "ca85c06a-ecec-453c-af7d-6ba21b52bca1", "AQAAAAEAACcQAAAAEB5iAjexrq0DwpTqqUb12WflKeDqrzqNvjhx0cdEyYb7ym+M9SKtIHqUzkbskKtJFQ==", new DateTime(2020, 6, 28, 19, 7, 0, 640, DateTimeKind.Local).AddTicks(2687) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 28, 18, 7, 0, 627, DateTimeKind.Utc).AddTicks(4316));
        }
    }
}
