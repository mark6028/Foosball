using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class MatchDTO : Entity, ITrackable
    {
        [Required]
        public MatchState State { get; set; }

        [Required]
        [Display(Name = "TeamGrey")]
        public int TeamGreyId { get; set; }

        [Required]
        [Display(Name = "TeamBlack")]
        public int TeamBlackId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }                
    }

    public class Match : MatchDTO
    {
        public virtual Team TeamGrey { get; set; }
        public virtual Team TeamBlack { get; set; }        
        public virtual List<Goal> Goals { get; set; }

        public string Description
        {
            get
            {
                if (TeamGrey == null || TeamBlack == null)
                {
                    if (TeamGreyId == 0 || TeamBlackId == 0)
                    {
                        return String.Format("Team #{0} vs. Team #{1}",
                            TeamGreyId,
                            TeamBlackId);
                    }
                    
                    return String.Format("Match #{0}", Id);
                }

                return String.Format("{0} vs. {1}",
                    TeamGrey.Description,
                    TeamBlack.Description);
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if (CreatedAt == null || LastUpdatedAt == null)
                    return new TimeSpan(0);

                TimeSpan duration = (DateTime)LastUpdatedAt - (DateTime)CreatedAt;

                return duration;
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
