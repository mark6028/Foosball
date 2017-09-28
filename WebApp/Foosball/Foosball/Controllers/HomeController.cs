using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Foosball.Models;
using Microsoft.EntityFrameworkCore;

namespace Foosball.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        private readonly FoosballContext _context;

        public HomeController(FoosballContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Tiles data
            ViewData["TotalGoals"] = _context.Goal.Count();
            ViewData["TotalMatches"] = _context.Match.Count();
            ViewData["TotalPlayers"] = _context.Player.Count();
            var matches = _context.Match
               .Where(m => m.State == MatchState.Completed)
                .Select(m => new Match
                {
                    CreatedAt = m.CreatedAt,
                    LastUpdatedAt = m.LastUpdatedAt
                })
                .ToList();

            double averageDuration = matches.Average(m => m.Duration.TotalSeconds);
            ViewData["AvgMatchDuration"] = TimeSpan.FromSeconds(averageDuration).ToString(@"hh\:mm\:ss"); ;
            
            var idQueryTopPlayerRatings = _context.Rating
                .GroupBy(r => r.PlayerId)
                .Select(g => g.OrderByDescending(r => r.LastUpdatedAt).FirstOrDefault())
                .OrderByDescending(r => r.ELO)
                .Take(5)
                .Select(r => r.Id);

            ViewData["TopPlayerRatings"] = _context.Rating
                .Include("Player")
                .Where(r => idQueryTopPlayerRatings.Contains(r.Id))
                .OrderByDescending(r => r.ELO);

            var idQueryTopTeamsRatings = _context.Rating
                .GroupBy(r => r.TeamId)
                .Select(g => g.OrderByDescending(r => r.LastUpdatedAt).FirstOrDefault())
                //.Include(c => c.Player)
                .OrderByDescending(r => r.ELO)
                .Take(5)
                .Select(r => r.Id);

            ViewData["TopTeamRatings"] = _context.Rating
                .Include(r => r.Team)
                    .ThenInclude(t => t.PlayerOne)
                .Include(r => r.Team)
                    .ThenInclude(t => t.PlayerTwo)
                .Where(r => idQueryTopTeamsRatings.Contains(r.Id) && r.TeamId != null)
                .OrderByDescending(r => r.ELO); 

            ViewData["CompletedMatches"] = _context.Match
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
                .Take(10);

            return View();
        }

        [HttpGet]
        [Route("/Error/{status}")]
        public IActionResult Error(int status) { 
            ViewData["status"] = status;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
