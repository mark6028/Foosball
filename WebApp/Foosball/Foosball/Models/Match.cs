using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public MatchState State { get; set; }

        [Required]
        [Display(Name = "TeamGrey")]
        public int TeamGreyId { get; set; }
        public Team TeamGrey { get; set; }

        [Required]
        [Display(Name = "TeamBlack")]
        public int TeamBlackId { get; set; }
        public Team TeamBlack { get; set; }
        
        public List<Goal> Goals { get; set; }

        public string Description
        {
            get
            {
                if (TeamGrey == null || TeamBlack == null)
                    return String.Format("Match #{0}", Id);

                return String.Format("{0} vs. {1}",
                    TeamGrey.Description,
                    TeamBlack.Description);
            }
        }
    }

    public enum MatchState
    {
        Setup,
        Started,
        Completed
    }
}
