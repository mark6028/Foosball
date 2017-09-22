using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class Player : ITrackable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Tag { get; set; }      

        public virtual List<Goal> Goals { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
