using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineGest.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Age { get; set; }

        public string Image { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleFK { get; set; }
        public Roles Role { get; set; }
    }
}
