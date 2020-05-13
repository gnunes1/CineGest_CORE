using CineGest.Models;
using Microsoft.EntityFrameworkCore;

namespace CineGest.Data
{
    /// <summary>
    /// classe que representa a BD
    /// </summary>
    public class CineGestDB : DbContext
    {
        /// <summary>
        /// construtor da classe
        /// relaciona a classe à DB
        /// </summary>
        /// <param name="options"></param>
        public CineGestDB(DbContextOptions<CineGestDB> options) : base(options)
        {
        }

        public DbSet<Cinemas> Cinema { get; set; }

        public DbSet<Cinema_Movie> Cinema_Movie { get; set; }

        public DbSet<Cities> City { get; set; }

        public DbSet<Movies> Movie { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Rooms> Room { get; set; }

        public DbSet<Room_Movie> Room_Movie { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        public DbSet<User> User { get; set; }


        //Faz a ligação das chaves extrangeiras compostas das tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //----------Cinema_Movie-----------------

            //ForeignKey(MovieFK) references Movie(id)
            modelBuilder.Entity<Cinema_Movie>()
                .HasOne(c_m => c_m.Movie).WithMany(m => m.CinemaMovieList).HasForeignKey(c_m => c_m.MovieFK);

            //ForeignKey(CinemaFK) references Cinema(id)
            modelBuilder.Entity<Cinema_Movie>()
                .HasOne(c_m => c_m.Cinema).WithMany(m => m.CinemaList).HasForeignKey(c_m => c_m.CinemaFK);

            //PrimaryKey(movie_id, cinema_id)
            modelBuilder.Entity<Cinema_Movie>()
                .HasKey(c_m => new { c_m.MovieFK, c_m.CinemaFK });

            //---------------------Rooms-------------------------

            ////ForeignKey(CinemaFK) references Cinema(id)
            modelBuilder.Entity<Rooms>()
                .HasOne(r => r.Cinema).WithMany(c => c.RoomList).HasForeignKey(r => r.CinemaFK);

            //PrimaryKey(Id, idCinema)
            modelBuilder.Entity<Rooms>()
                .HasKey(r => new { r.Id, r.CinemaFK });

            //--------------------Room_Movie-----------------------

            //ForeignKey(MovieFK) references Movie(id)
            modelBuilder.Entity<Room_Movie>()
                .HasOne(r_m => r_m.Movie).WithMany(m => m.RoomMovieList).HasForeignKey(c_m => c_m.MovieFK);

            //ForeignKey(RoomFK) references Room(id)
            //ForeignKey(CinemaFK) references Room(Cinema)
            modelBuilder.Entity<Room_Movie>()
                .HasOne(r_m => r_m.Room).WithMany(r => r.RoomMovieList).HasForeignKey(c_m => new { c_m.RoomFK, c_m.CinemaFK });

            //PrimaryKey(Movie, Room)
            modelBuilder.Entity<Room_Movie>()
                .HasKey(r_m => new { r_m.MovieFK, r_m.RoomFK });

            //---------------------Ticket--------------------------

            //ForeignKey(MovieFK) references Room_Movie(id)
            //ForeignKey(RoomFK) references Room_Movie(id)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Room_MovieMovie).WithMany(r_m => r_m.Room_MovieList).HasForeignKey(t => new { t.MovieFK, t.RoomFK });

            //ForeignKey(UserFK) references User(id)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User).WithMany(r_m => r_m.TicketList).HasForeignKey(t => t.UserFK);

            //PrimaryKey(Movie, Room, User)
            modelBuilder.Entity<Ticket>()
                .HasKey(t => new { t.MovieFK, t.RoomFK, t.UserFK });
        }
    }
}
