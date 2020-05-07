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

        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Cinema_Movie> Cinema_Movie { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Room_Movie> Room_Movie { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Função para criar chaves compostas
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema_Movie>()
                .HasKey(c_m => new { c_m.MovieFK, c_m.CinemaFK });
            modelBuilder.Entity<Room>()
                .HasKey(r => new { r.Id, r.CinemaFK });
            modelBuilder.Entity<Room_Movie>()
                .HasKey(r_m => new { r_m.MovieFK, r_m.RoomFK });
            modelBuilder.Entity<Ticket>()
                .HasKey(t => new { t.RoomFK, t.MovieFK, t.UserFK });
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Room_Movie).WithMany(r_m => r_m.TicketList).HasForeignKey(r_m => new { r_m.MovieFK, r_m.RoomFK });
        }
    }
}
