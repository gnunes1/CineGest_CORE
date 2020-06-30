using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Users
    {
        public Users()
        {
            TicketsList = new HashSet<Tickets>();
        }

        /// <summary>
        /// Referencia o utilizador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        public DateTime DoB { get; set; }

        /// <summary>
        /// nome da fotografia do utilizador
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Cargo do utilizador
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Cargo do utilizador
        /// </summary>
        public string ApplicationUser { get; set; }

        /// <summary>
        /// Lista de bilhetes associados ao utilizador
        /// </summary>
        public ICollection<Tickets> TicketsList { get; set; }
    }
}
