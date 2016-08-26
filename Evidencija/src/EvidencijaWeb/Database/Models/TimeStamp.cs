///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evidencija.Database.Models
{
    public class TimeStamp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Duration { get; set; }

        public bool Closed { get; set; }

        public User User { get; set; }
    }
}
