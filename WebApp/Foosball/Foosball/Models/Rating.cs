using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class RatingDTO : Entity, ITrackable
    {
        [Display(Name = "Player")]
        public int? PlayerId { get; set; }

        [Display(Name = "Team")]
        public int? TeamId { get; set; }

        public float ELO { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }

    public class Rating : RatingDTO
    {    
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
