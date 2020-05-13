using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Cities
    {
        /// <summary>
        /// Referencia a cidade do cinema
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da cidade do cinema
        /// </summary>
        [Required]
        public string Name { get; set; }

    }
}
