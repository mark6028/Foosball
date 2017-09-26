using Foosball.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Foosball.Controllers
{
    public class PlayersController : Controller
    {
        private readonly FoosballContext _context;

        public PlayersController(FoosballContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var idQuery = _context.Rating
                .GroupBy(r => r.PlayerId)
                .Select(g => g.OrderByDescending(r => r.LastUpdatedAt).FirstOrDefault())
                .OrderByDescending(r => r.ELO)
                .Select(r => r.Id);

            var players = _context.Rating
                .Include("Player")
                .Where(r => idQuery.Contains(r.Id))
                .OrderByDescending(r => r.ELO)
                .Where(r => r.PlayerId > 0)
                .Select(r => new Rating
                {
                    ELO = r.ELO,
                    Player = new Player
                    {
                        Id = r.Player.Id,
                        Name = r.Player.Name
                    }
                });

            return View(players);
        }

        public IActionResult Profile(int id)
        {
            ViewData["Player"] = _context.Player
                .Include(p => p.Ratings)
                .Where(p => p.Id == id)
                .Select(p => new Player
                {
                    Id = p.Id,
                    Name = p.Name,
                    Ratings = new List<Rating>
                    {
                        new Rating { ELO = p.Ratings[0].ELO }
                    }
                })
                .FirstOrDefault();


            var teams = _context.Team
                .Include(t => t.PlayerOne)
                .Include(t => t.PlayerTwo)
                .Where(p => p.PlayerOneId == id || p.PlayerTwoId == id);

            ViewData["Teams"] = teams;

            var idQuery = teams.Select(r => r.Id);

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
                .Where(r => idQuery.Contains(r.Id));

            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }
    }
}
