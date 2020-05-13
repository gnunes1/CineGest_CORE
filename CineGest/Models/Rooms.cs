using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Rooms
    {
        /// <summary>
        /// Referencia a sala de filme
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Relaciona a sala ao cinema
        /// </summary>
        public int CinemaFK { get; set; }

        /// <summary>
        /// Referencia o cinema
        /// </summary>
        public Cinemas Cinema { get; set; }

        /// <summary>
        /// Capacidade de alojamento da sala de cinema
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// Numero da sala de cinema
        /// </summary>
        [Required]
        public int Room_number { get; set; }

        /// <summary>
        /// Lista de salas a serem usados para mostrar os filmes
        /// </summary>
        public ICollection<Room_Movie> RoomMovieList { get; set; }
    }
}
