using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(City))]
        public int CityFK { get; set; }
        public City City { get; set; }

    }
}
