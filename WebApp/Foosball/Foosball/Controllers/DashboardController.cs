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
            return View();            
        }
    }
}