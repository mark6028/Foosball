using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;
using Microsoft.AspNetCore.Authorization;

namespace Foosball.Controllers
{
    public class TeamsController : Controller
    {
        private readonly FoosballContext _context;

        public TeamsController(FoosballContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var idQuery = _context.Rating
                .GroupBy(r => r.TeamId)
                .Select(g => g.OrderByDescending(r => r.LastUpdatedAt).FirstOrDefault())
                .OrderByDescending(r => r.ELO)
                .Select(r => r.Id);

            var teams = _context.Rating
                .Include(r => r.Team)
                    .ThenInclude(t => t.PlayerOne)
                .Include(r => r.Team)
                    .ThenInclude(t => t.PlayerTwo)
                .Where(r => idQuery.Contains(r.Id))
                .OrderByDescending(r => r.ELO)
                .Where(r => r.TeamId > 0)
                .Select(r => new Rating
                {
                    ELO = r.ELO,
                    Team = new Team
                    {
                        Id = r.Team.Id,
                        PlayerOne = new Player {
                            Name = r.Team.PlayerOne.Name
                        },
                        PlayerTwo = new Player
                        {
                            Name = r.Team.PlayerTwo.Name
                        }
                    }
                });

            return View(teams);
        }

        public IActionResult Profile(int id)
        {            
            var team = _context.Team
                .Include(t => t.PlayerOne)
                .Include(t => t.PlayerTwo)
                .Where(p => p.Id == id)
                .Select(t => new Team
                {
                    Id = t.Id,
                    PlayerOne = new Player
                    {
                        Name = t.PlayerOne.Name
                    },
                    PlayerTwo = new Player
                    {
                        Name = t.PlayerTwo.Name
                    }
                })
                .FirstOrDefault();

            if (team == null)
                return BadRequest("Invalid Team ID");

            ViewData["Team"] = team;

            ViewData["Ratings"] = _context.Rating
                .Where(r => r.TeamId == id)
                .Select(r => new Rating
                {
                    TeamId = r.TeamId,
                    ELO = r.ELO,
                    CreatedAt = r.CreatedAt,
                    LastUpdatedAt = r.LastUpdatedAt
                });                       

            ViewData["TeamMatches"] = _context.Match
                .Where(m => m.State == MatchState.Completed)
                .Include(m => m.Goals)
                .Include(m => m.TeamBlack)
                    .ThenInclude(t => t.PlayerOne)
                .Include(m => m.TeamBlack)
                    .ThenInclude(t => t.PlayerTwo)
                .Include(m => m.TeamGrey)
                    .ThenInclude(t => t.PlayerOne)
                .Include(m => m.TeamGrey)
                    .ThenInclude(t => t.PlayerTwo)
                .Take(10)
                .Where(r => r.TeamBlackId == id || r.TeamGreyId == id);

            return View();
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
