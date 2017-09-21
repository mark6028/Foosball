using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Foosball.Models;

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
            ViewData["TopPlayers"] = _context.Player.Take(10).OrderBy(p => p.Name);
            ViewData["TopTeams"] = _context.Team.Take(10).OrderBy(t => t.Description);

            return View();            
        }
    }
}