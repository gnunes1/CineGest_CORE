using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class User
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
        /// Password do utilizador
        /// </summary>
        [Required]
        public string Password { get; set; }

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
        /// Relaciona o utilizador a um cargo
        /// </summary>
        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleFK { get; set; }

        /// <summary>
        /// Referência o cargo
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        /// Lista de bilhetes assiciados ao utilizador
        /// </summary>
        public ICollection<Ticket> TicketList { get; set; }
    }
}
