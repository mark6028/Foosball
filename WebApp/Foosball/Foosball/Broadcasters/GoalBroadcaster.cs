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

        public async Task BroadcastGoalCreated(Goal goal)
        {
            var goalObject = await _context.Goal
                .Include(g => g.Player)
                .Include(g => g.Match)
                .SingleOrDefaultAsync(g => g.Id == goal.Id);

            var goalJson = ConvertToJSON<Goal>(goalObject);

            await _goalhubContext.Clients.All.InvokeAsync("Send", goalJson);
        }

        private string ConvertToJSON<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity,
                Formatting.Indented, 
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
        }
    }
}
