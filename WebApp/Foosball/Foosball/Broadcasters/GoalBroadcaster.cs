using Foosball.Hubs;
using Foosball.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foosball.Broadcasters
{
    public class GoalBroadcaster : IGoalBroadcaster
    {
        private readonly FoosballContext _context;
        private readonly IHubContext<GoalHub> _goalhubContext;

        public GoalBroadcaster(FoosballContext context, IHubContext<GoalHub> goalhubContext)
        {
            _context = context;
            _goalhubContext = goalhubContext;
        }

        public async Task GoalScored(Goal goal)
        {
            var goalObject = _context.Goal
                .Include(g => g.Player)
                .Include(g => g.Match)
                .Select(g => new Goal
                {
                    Id = g.Id,
                    TeamColor = g.TeamColor,
                    Position = g.Position,
                    Player = new Player
                    {
                        Id = g.Player.Id,
                        Name = g.Player.Name
                    },
                    Match = new Match
                    {
                        Id = g.Match.Id
                    }
                }).SingleOrDefault(g => g.Id == goal.Id);

            await _goalhubContext.Clients.All.InvokeAsync("GoalScored", goalObject);
        }        
    }
}
