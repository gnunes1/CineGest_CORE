using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Cinema_Movie
    {
        [ForeignKey(nameof(Movie))]
        public int MovieFK { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey(nameof(Cinema))]
        public int CinemaFK { get; set; }
        public Cinema Cinema { get; set; }

    }
}
