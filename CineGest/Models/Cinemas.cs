using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Cinemas
    {
        /// <summary>
        /// Referencia o cinema
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do cinema
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Cidade onde o cinema está situado
        /// </summary>
        [Required]
        [ForeignKey(nameof(City))]
        public int CityFK { get; set; }

        /// <summary>
        /// Referência a cidade
        /// </summary>
        public Cities City { get; set; }

        /// <summary>
        /// Lista dos filmes no cinema
        /// </summary>
        public ICollection<Cinema_Movie> CinemaList { get; set; }

        /// <summary>
        /// Lista de salas do cinema
        /// </summary>
        public ICollection<Rooms> RoomList { get; set; }
    }
}
