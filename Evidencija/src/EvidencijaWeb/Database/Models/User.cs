///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evidencija.Database.Models
{
    public class User
    {
        public User()
        {
            UserTimeStamps = new List<TimeStamp>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int LoginKey { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<TimeStamp> UserTimeStamps { get; set; }
    }
}
