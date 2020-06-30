using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Cinemas
    {
        public Cinemas()
        {
            SessionsList = new HashSet<Sessions>();
        }

        /// <summary>
        /// Referencia o cinema
        /// </summary>
        [Key]
        [Display(Name = "# cinema")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do cinema
        /// </summary>
        [RegularExpression("^[a-z][0-9]$", ErrorMessage = "O nome tem de começar por uma letra.")]
        [Required(ErrorMessage = "O nome é de preenchimento obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(40, ErrorMessage = "O nome não pode ter mais de {1} carateres.")]
        [Remote(action: "VerifyName", controller: "Cinemas")]
        public string Name { get; set; }

        /// <summary>
        /// Capacidade do cinema
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Capacidade")]
        public int Capacity { get; set; }

        /// <summary>
        /// Nome da cidade
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        /// <summary>
        /// Localização do cinema
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Localização")]
        public string Location { get; set; }

        /// <summary>
        /// Lista das sessões no cinema
        /// </summary>
        public ICollection<Sessions> SessionsList { get; set; }

    }
}
