using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class TeamDTO : Entity
    {
        [Required]
        [Display(Name = "Player 1")]
        public int PlayerOneId { get; set; }

        [Display(Name = "Player 2")]
        public int PlayerTwoId { get; set; }        
    }

    public class Team : TeamDTO
    {
        [Display(Name = "Player 1")]
        public virtual Player PlayerOne { get; set; }

        [Display(Name = "Player 2")]
        public virtual Player PlayerTwo { get; set; }

        public virtual List<Rating> Ratings { get; set; }

        public string Description
        {
            get
            {
                if (PlayerOne == null || PlayerTwo == null)
                    return String.Format("Team #{0}", Id);

                return String.Format("{0} & {1}",
                    PlayerOne.Name,
                    PlayerTwo.Name);
            }
        }
    }
}
