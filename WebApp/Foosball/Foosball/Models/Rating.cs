using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Player")]
        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public float ELO { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }
    }
}
