using Foosball.Models;
using Microsoft.AspNetCore.Authorization;
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
            var player = _context.Player
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (player == null)
                return BadRequest("Invalid Player ID");

            ViewData["Player"] = player;

            ViewData["Ratings"] = _context.Rating
                .Where(r => r.PlayerId == id)
                .Select(r => new Rating
                {
                    PlayerId = r.PlayerId,
                    ELO = r.ELO,
                    CreatedAt = r.CreatedAt,
                    LastUpdatedAt = r.LastUpdatedAt
                });

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

        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
