using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Room_Movie
    {

        [ForeignKey(nameof(Movie))]
        public int MovieFK { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey(nameof(Room))]
        public int RoomFK { get; set; }
        public int Room { get; set; }

        [Required]
        public int Schedule { get; set; }


        public ICollection<Ticket> TicketList { get; set; }
    }
}
