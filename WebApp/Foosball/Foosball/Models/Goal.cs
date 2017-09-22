using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class Goal : ITrackable
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Match")]
        public int MatchId { get; set; }
        public Match Match { get; set; }

        [Required]
        public GoalPosition Position { get; set; }

        [Required]
        [Display(Name = "Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }

    public enum GoalPosition
    {
        Defense,
        Offense
    }
}
