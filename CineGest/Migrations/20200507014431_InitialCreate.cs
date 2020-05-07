using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CineGest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Genres = table.Column<string>(nullable: true),
                    Duration = table.Column<DateTime>(nullable: false),
                    Min_age = table.Column<int>(nullable: false),
                    Highlighted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cinema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CityFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cinema_City_CityFK",
                        column: x => x.CityFK,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room_Movie",
                columns: table => new
                {
                    MovieFK = table.Column<int>(nullable: false),
                    RoomFK = table.Column<int>(nullable: false),
                    Room = table.Column<int>(nullable: false),
                    Schedule = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room_Movie", x => new { x.MovieFK, x.RoomFK });
                    table.ForeignKey(
                        name: "FK_Room_Movie_Movie_MovieFK",
                        column: x => x.MovieFK,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Age = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    RoleFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_RoleFK",
                        column: x => x.RoleFK,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cinema_Movie",
                columns: table => new
                {
                    MovieFK = table.Column<int>(nullable: false),
                    CinemaFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinema_Movie", x => new { x.MovieFK, x.CinemaFK });
                    table.ForeignKey(
                        name: "FK_Cinema_Movie_Cinema_CinemaFK",
                        column: x => x.CinemaFK,
                        principalTable: "Cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cinema_Movie_Movie_MovieFK",
                        column: x => x.MovieFK,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CinemaFK = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Room_number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => new { x.Id, x.CinemaFK });
                    table.ForeignKey(
                        name: "FK_Room_Cinema_CinemaFK",
                        column: x => x.CinemaFK,
                        principalTable: "Cinema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    RoomFK = table.Column<int>(nullable: false),
                    MovieFK = table.Column<int>(nullable: false),
                    UserFK = table.Column<int>(nullable: false),
                    seat_number = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => new { x.RoomFK, x.MovieFK, x.UserFK });
                    table.ForeignKey(
                        name: "FK_Ticket_User_UserFK",
                        column: x => x.UserFK,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Room_Movie_MovieFK_RoomFK",
                        columns: x => new { x.MovieFK, x.RoomFK },
                        principalTable: "Room_Movie",
                        principalColumns: new[] { "MovieFK", "RoomFK" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinema_CityFK",
                table: "Cinema",
                column: "CityFK");

            migrationBuilder.CreateIndex(
                name: "IX_Cinema_Movie_CinemaFK",
                table: "Cinema_Movie",
                column: "CinemaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Room_CinemaFK",
                table: "Room",
                column: "CinemaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserFK",
                table: "Ticket",
                column: "UserFK");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MovieFK_RoomFK",
                table: "Ticket",
                columns: new[] { "MovieFK", "RoomFK" });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleFK",
                table: "User",
                column: "RoleFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cinema_Movie");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Cinema");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Room_Movie");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
