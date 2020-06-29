﻿using CineGest.Models;
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
        public CinegestDB(DbContextOptions<CinegestDB> options) : base(options)
        {
        }

        public DbSet<Cinemas> Cinemas { get; set; }

        public DbSet<Sessions> Sessions { get; set; }

        public DbSet<Movies> Movies { get; set; }

        public DbSet<Tickets> Tickets { get; set; }

        public DbSet<Users> Users { get; set; }

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

            //adiciona o role de admin e user
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                });

            //cria o Application user: admin
            builder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser
                {
                    Id = "1",
                    Nome = "Admin",
                    UserName = "admin@admin",
                    NormalizedUserName = "admin@admin".ToUpper(),
                    NormalizedEmail = "admin@admin".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "admin"),
                    SecurityStamp = string.Empty,
                    Timestamp = DateTime.Now
                });

            //associa o role: Admin ao ApplicationUser: Admin
            builder.Entity<IdentityUserRole<string>>(b =>
            {
                builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "1"
                });

                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNetUserRoles table
                b.ToTable("AspNetUserRoles");
            });

        }
    }
}
