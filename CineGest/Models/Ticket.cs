using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Ticket
    {
        //PrimaryKey(RoomFK, MovieFK, UserFK)

        /// <summary>
        /// Relaciona a sala do cinema
        /// </summary>
        public int RoomFK { get; set; }

        /// <summary>
        /// Referência a sala do cinema
        /// </summary>
        public Room_Movie Room_MovieRoom { get; set; }

        /// <summary>
        /// Relaciona o filme na sala
        /// </summary>
        public int MovieFK { get; set; }

        /// <summary>
        /// Referência o filme na sala
        /// </summary>
        public Room_Movie Room_MovieMovie { get; set; }

        /// <summary>
        /// Relaciona o utilizador com o bilhete
        /// </summary>
        public int UserFK { get; set; }

        /// <summary>
        /// Referência o utilizador com o bilhete
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Numero do banco da sala onde o filme sera reproduzido
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto increment
        public int seat_number { get; set; }

    }
}
