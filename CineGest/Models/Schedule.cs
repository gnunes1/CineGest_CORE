using System;
using System.ComponentModel.DataAnnotations;

namespace CineGest.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }

    }
}
