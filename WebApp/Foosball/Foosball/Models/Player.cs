using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class PlayerDTO : Entity, ITrackable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Tag { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }

    public class Player : PlayerDTO
    {
        public virtual List<Goal> Goals { get; set; }
        public virtual List<Rating> Ratings { get; set; }                
    }
}
