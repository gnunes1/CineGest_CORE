using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Users
    {
        /// <summary>
        /// Referencia o utilizador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Idade do utilizador
        /// </summary>
        [Required]
        public DateTime Age { get; set; }

        /// <summary>
        /// Caminho da fotografia do utilizador
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Referência o cargo
        /// </summary>
        [Required]
        public int Role { get; set; }

        /// <summary>
        /// Lista de bilhetes associados ao utilizador
        /// </summary>
        public ICollection<Tickets> TicketList { get; set; }
    }
}
