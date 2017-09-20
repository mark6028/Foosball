using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;

namespace Foosball.Models
{
    public class FoosballContext : DbContext
    {
        public FoosballContext (DbContextOptions<FoosballContext> options)
            : base(options)
        {
        }

        public DbSet<Foosball.Models.Player> Player { get; set; }

        public DbSet<Foosball.Models.Team> Team { get; set; }

        public DbSet<Foosball.Models.Match> Match { get; set; }

        public DbSet<Foosball.Models.Goal> Goal { get; set; }
    }
}
