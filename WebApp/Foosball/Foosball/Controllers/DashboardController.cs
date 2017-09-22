using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Foosball.Models;
using Microsoft.EntityFrameworkCore;

namespace Foosball.Controllers
{
    public class DashboardController : Controller
    {
        private readonly FoosballContext _context;

        public DashboardController(FoosballContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Tiles data
            ViewData["TotalGoals"] = _context.Goal.Count();
            ViewData["TotalMatches"] = _context.Match.Count();
            ViewData["TotalPlayers"] = _context.Player.Count();
            ViewData["AvgMatchDuration"] = 5.3; // _context.Match.Count();

            //Scoreboard data
            /*ViewData["TopPlayers"] = _context.Player.
                FromSql("SELECT Player.Id, Player.Name, Player.Tag, r1.ELO, r1.Timestamp " +
                    "FROM Player " +
                    "INNER JOIN Rating as r1 ON r1.PlayerId = Player.Id " +
                    "LEFT OUTER JOIN Rating as r2 ON r2.PlayerId = Player.Id AND r1.Timestamp < r2.Timestamp " +
                    "WHERE r2.Id IS NULL " +
                    "ORDER BY r1.ELO DESC");*/

            //ViewData["TopPlayers"] 
            var idQuery = _context.Rating
                .GroupBy(r => r.PlayerId)
                .Select(g => g.OrderByDescending(r => r.LastUpdatedAt).FirstOrDefault())
                //.Include(c => c.Player)
                .OrderByDescending(r => r.ELO)
                .Take(10)
                .Select(r => r.Id);

            ViewData["TopPlayerRatings"] = _context.Rating
                .Include("Player")
                .Where(r => idQuery.Contains(r.Id))
                .OrderByDescending(r => r.ELO);      

            /*ViewData["TopPlayers"] = _context.Player                
                .Select(p => new Player
                {
                    Id = p.Id,
                    Name = p.Name,
                    Ratings = p.Ratings.OrderByDescending(r => r.Timestamp).Take(1).ToList()
                })
                .OrderByDescending(p => p.Ratings[0].ELO)
                .Take(10);*/

            ViewData["TopTeams"] = _context.Team
                .Include(t => t.PlayerOne)
                .Include(t => t.PlayerTwo)
                .OrderBy(t => t.Description)
                .Take(10);

            return View();            
        }
    }
}