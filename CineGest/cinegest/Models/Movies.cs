﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Movies
    {
        public Movies()
        {
            SessionsList = new HashSet<Sessions>();
        }

        /// <summary>
        /// Referencia o filme
        /// </summary>
        [Key]
        [Display(Name = "# filme")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do filme
        /// </summary>
        [RegularExpression("^([A-ZÓÂÍa-zçáéíóúàèìòùãõäëïöüâêîôûñ].*)", ErrorMessage = "O nome tem de começar por uma letra.")]
        [Required(ErrorMessage = "O nome é de preenchimento obrigatório.")]
        [StringLength(40, ErrorMessage = "O nome não pode ter mais de {1} carateres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        /// <summary>
        /// Descricao do filme
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        /// <summary>
        /// gêneros do filme
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Gêneros")]
        [RegularExpression("[a-zA-Z]+(;\\s[a-zA-Z]+)*", ErrorMessage = "Cada gênero só pode conter letras e têm de ser separados com \";\" e de seguida um espaço")]
        public string Genres { get; set; }

        /// <summary>
        /// nome do cartaz do filme
        /// </summary>
        [Display(Name = "Cartaz")]
        public string Poster { get; set; }

        /// <summary>
        /// Duracao do filme em minutos
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Duração (em minutos)")]
        public int Duration { get; set; }

        /// <summary>
        /// Idade minima para assistir ao filme
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Idade mínima")]
        public int Min_age { get; set; }

        /// <summary>
        /// verifica filme está em destaque
        /// </summary>
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [Display(Name = "Em destaque")]
        public bool Highlighted { get; set; }

        /// <summary>
        /// Todas as sessões do filme
        /// </summary>
        public ICollection<Sessions> SessionsList { get; set; }

    }
}
