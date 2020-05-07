using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Ticket
    {

        public int RoomFK { get; set; }
        public int MovieFK { get; set; }

        public Room_Movie Room_Movie { get; set; }

        [ForeignKey(nameof(User))]
        public int UserFK { get; set; }
        public User User { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto increment
        public int seat_number { get; set; }

    }
}
