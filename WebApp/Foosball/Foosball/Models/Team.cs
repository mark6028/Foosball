using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Player 1")]
        public int PlayerOneId { get; set; }

        [Display(Name = "Player 1")]
        public Player PlayerOne { get; set; }

        [Display(Name = "Player 2")]
        public int PlayerTwoId { get; set; }

        [Display(Name = "Player 2")]
        public Player PlayerTwo { get; set; }

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
