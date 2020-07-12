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
        [Display(Name = "# Utilizador")]
        public int Id { get; set; }

        /// <summary>
        /// Email do utilizador
        /// </summary>
        [Required(ErrorMessage = "O email é de preenchimento obrigatório.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Nome do utilizador
        /// </summary>
        [Required(ErrorMessage = "O nome é de preenchimento obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(40, ErrorMessage = "O nome não pode ter mais de {1} carateres.")]
        [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                          ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
        public string Name { get; set; }

        /// <summary>
        /// Data de nascimento do utilizador
        /// </summary>
        [Required(ErrorMessage = "A data de nascimento é de preenchimento obrigatório.")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        /// <summary>
        /// nome da fotografia do utilizador
        /// </summary>
        [Display(Name = "Fotografia")]
        public string Avatar { get; set; }

        /// <summary>
        /// Cargo do utilizador
        /// </summary>
        [Display(Name = "Cargo")]
        public string Role { get; set; }

        /// <summary>
        /// Lista de bilhetes associados ao utilizador
        /// </summary>
        public ICollection<Tickets> TicketsList { get; set; }
    }
}
