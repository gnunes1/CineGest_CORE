using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cinegest.Data.Migrations
{
    public partial class AdminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Cinema_CinemaFK",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movie_MovieFK",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Sessions_SessionFK",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_UserFK",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cinema",
                table: "Cinema");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Ticket",
                newName: "Tickets");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "Cinema",
                newName: "Cinemas");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_UserFK",
                table: "Tickets",
                newName: "IX_Tickets_UserFK");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_SessionFK",
                table: "Tickets",
                newName: "IX_Tickets_SessionFK");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_Name",
                table: "Movies",
                newName: "IX_Movies_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Cinema_Name",
                table: "Cinemas",
                newName: "IX_Cinemas_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cinemas",
                table: "Cinemas",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "eaf469a3-b694-4f42-814c-72dc7d50ce70", "Admin", "ADMIN" },
                    { "2", "84191db0-eaba-41cf-b473-153a3de5ce1e", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Timestamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "ca85c06a-ecec-453c-af7d-6ba21b52bca1", null, true, false, null, "Admin", "ADMIN@ADMIN", "ADMIN@ADMIN", "AQAAAAEAACcQAAAAEB5iAjexrq0DwpTqqUb12WflKeDqrzqNvjhx0cdEyYb7ym+M9SKtIHqUzkbskKtJFQ==", null, false, "", new DateTime(2020, 6, 28, 19, 7, 0, 640, DateTimeKind.Local).AddTicks(2687), false, "admin@admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 28, 18, 7, 0, 627, DateTimeKind.Utc).AddTicks(4316));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Cinemas_CinemaFK",
                table: "Sessions",
                column: "CinemaFK",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieFK",
                table: "Sessions",
                column: "MovieFK",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Sessions_SessionFK",
                table: "Tickets",
                column: "SessionFK",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserFK",
                table: "Tickets",
                column: "UserFK",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Cinemas_CinemaFK",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieFK",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Sessions_SessionFK",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserFK",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cinemas",
                table: "Cinemas");

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

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "Cinemas",
                newName: "Cinema");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_UserFK",
                table: "Ticket",
                newName: "IX_Ticket_UserFK");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SessionFK",
                table: "Ticket",
                newName: "IX_Ticket_SessionFK");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_Name",
                table: "Movie",
                newName: "IX_Movie_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Cinemas_Name",
                table: "Cinema",
                newName: "IX_Cinema_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                table: "Ticket",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cinema",
                table: "Cinema",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoB",
                value: new DateTime(2020, 6, 27, 0, 7, 46, 55, DateTimeKind.Utc).AddTicks(7028));

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Cinema_CinemaFK",
                table: "Sessions",
                column: "CinemaFK",
                principalTable: "Cinema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movie_MovieFK",
                table: "Sessions",
                column: "MovieFK",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Sessions_SessionFK",
                table: "Ticket",
                column: "SessionFK",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_UserFK",
                table: "Ticket",
                column: "UserFK",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
