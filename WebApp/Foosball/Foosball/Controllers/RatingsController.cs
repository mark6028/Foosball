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
    public class RatingsController : Controller
    {
        private readonly FoosballContext _context;

        public RatingsController(FoosballContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
