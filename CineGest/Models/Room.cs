using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Room
    {

        public int Id { get; set; }

        [ForeignKey(nameof(Cinema))]
        public int CinemaFK { get; set; }
        public Cinema Cinema { get; set; }

        [Required]
        public int Capacity { get; set; }
        [Required]
        public int Room_number { get; set; }
    }
}
