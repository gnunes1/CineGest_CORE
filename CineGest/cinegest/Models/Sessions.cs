using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    /// <summary>
    /// Sessão dos filmes
    /// </summary>
    public class Sessions
    {
        public Sessions()
        {
            TicketsList = new HashSet<Tickets>();
            MoviesList = new HashSet<Movies>();
            CinemasList = new HashSet<Cinemas>();
        }

        /// <summary>
        /// referência a sessão
        /// </summary>
        [Key]
        [Display(Name = "# Sessão")]
        public int Id { get; set; }

        /// <summary>
        /// Relaciona o cinema à sessão
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Cinema")]
        [ForeignKey(nameof(Cinema))]
        public int CinemaFK { get; set; }
        public Cinemas Cinema { get; set; }

        /// <summary>
        /// relaciona o filme à sessão
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Filme")]
        [ForeignKey(nameof(Movie))]
        public int MovieFK { get; set; }
        public Movies Movie { get; set; }

        /// <summary>
        /// data de início da sessão
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Data de início")]
        public DateTime Start { get; set; }

        /// <summary>
        /// data de fim da sessão
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Data de fim")]
        public DateTime End { get; set; }

        /// <summary>
        /// Número de lugares ocupados na sessão
        /// </summary>
        [Display(Name = "Lugares ocupados")]
        public int Occupated_seats { get; set; }

        /// <summary>
        /// Lista de bilhetes associados a esta sessão do filme
        /// </summary>
        public ICollection<Tickets> TicketsList { get; set; }
        public ICollection<Movies> MoviesList { get; set; }
        public ICollection<Cinemas> CinemasList { get; set; }

    }
}
