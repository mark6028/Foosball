using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;
using System.Threading;

namespace Foosball.Models
{
    public class FoosballContext : DbContext
    {
        public FoosballContext (DbContextOptions<FoosballContext> options)
            : base(options)
        {
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastUpdatedAt = now;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.LastUpdatedAt = now;
                            break;
                    }
                }
            }
        }

        public DbSet<Foosball.Models.Player> Player { get; set; }

        public DbSet<Foosball.Models.Team> Team { get; set; }

        public DbSet<Foosball.Models.Match> Match { get; set; }

        public DbSet<Foosball.Models.Goal> Goal { get; set; }

        public DbSet<Foosball.Models.Rating> Rating { get; set; }
    }
}
