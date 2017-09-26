using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;

namespace Foosball.Controllers
{
    public class TeamsController : Controller
    {
        private readonly FoosballContext _context;

        public TeamsController(FoosballContext context)
        {
            _context = context;
        }

        public IActionResult Admin()
        {
            return View();
        }
    }
}
