using CineGest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace cinegest.Data
{

    /// <summary>
    /// classe que extende da identity user acrescentado dados, criado a quando da Identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// nome do utilizador a registar
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// data de criação do utilizador a registar
        /// </summary>
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// classe que representa a BD
    /// </summary>
    public class CinegestDB : IdentityDbContext<ApplicationUser>
    {

        /// <summary>
        /// consturtor da classe
        /// liga a classe à BD
        /// </summary>
        /// <param name="options"></param>
        public CinegestDB(DbContextOptions<CinegestDB> options)
            : base(options)
        {
        }

        public DbSet<Cinemas> Cinema { get; set; }

        public DbSet<Sessions> Sessions { get; set; }

        public DbSet<Movies> Movie { get; set; }

        public DbSet<Tickets> Ticket { get; set; }

        public DbSet<Users> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // vai 'importar' o código anteriormente associado a este método
            // antes de o substituir pelo código seguinte
            base.OnModelCreating(builder);

            builder.Entity<Users>()
                 .HasIndex(u => u.Email)
                 .IsUnique();
            builder.Entity<Movies>()
                .HasIndex(m => m.Name)
                .IsUnique();
            builder.Entity<Cinemas>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<Users>().HasData(new
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@admin",
                RoleFK = "Admin",
                DoB = DateTime.UtcNow,
            });
        }
    }
}
