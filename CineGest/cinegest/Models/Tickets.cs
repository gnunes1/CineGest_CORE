using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Tickets
    {
        /// <summary>
        /// Id do bilhete
        /// </summary>
        [Key]
        [Display(Name = "# Bilhete")]
        public int Id { get; set; }

        /// <summary>
        /// Relaciona o bilhete com a sessão 
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "# Sessão")]
        [ForeignKey(nameof(Session))]
        public int SessionFK { get; set; }
        public Sessions Session { get; set; }

        /// <summary>
        /// Relaciona o utilizador com a sessão do filme
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Utilizador")]
        [ForeignKey(nameof(User))]
        public int UserFK { get; set; }
        public Users User { get; set; }

        /// <summary>
        /// Lugar respetivo ao bilhete comprado para a sessão
        /// </summary>
        [Required(ErrorMessage = "Este é de preenchimento obrigatório.")]
        [Display(Name = "Lugar")]
        public int Seat { get; set; }
    }
}
