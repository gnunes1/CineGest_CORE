using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{

    public class Roles
    {
        /// <summary>
        /// Referencia o cargo do utilizador
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome dos cargos para o utilizador
        /// </summary>
        [Required]
        public string Name { get; set; }

    }
}
