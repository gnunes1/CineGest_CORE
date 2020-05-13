using System;
using System.Collections.Generic;

namespace CineGest.Models
{
    /// <summary>
    /// Relaciona N salas a N cinemas
    /// </summary>
    public class Room_Movie
    {
        /// <summary>
        /// data e tempo de início do filme
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// data e tempo de fim do filme
        /// </summary>
        public DateTime End { get; set; }

        //PrimaryKey(RoomFK, MovieFK)
        /// <summary>
        /// relaciona o filme à sala
        /// </summary>
        public int MovieFK { get; set; }

        /// <summary>
        /// Referência o filme
        /// </summary>
        public Movies Movie { get; set; }

        /// <summary>
        ///  Relaciona a sala ao filme
        /// </summary>
        public int RoomFK { get; set; }

        /// <summary>
        /// Referência a sala
        /// </summary>
        public Rooms Room { get; set; }

        /// <summary>
        /// Relaciona o cinema da sala do filme ao filme
        /// </summary>
        public int CinemaFK { get; set; }

        /// <summary>
        /// Referencia o cinema da sala do filme
        /// </summary>
        public Rooms Cinema { get; set; }


        /// <summary>
        /// Lista de bilhetes associados a esta sessão do filme
        /// </summary>
        public ICollection<Ticket> Room_MovieList { get; set; }

    }
}
