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
    public class MatchBroadcaster : IMatchBroadcaster
    {
        private readonly FoosballContext _context;
        private readonly IHubContext<MatchHub> _matchhubContext;

        public MatchBroadcaster(FoosballContext context, IHubContext<MatchHub> matchhubContext)
        {
            _context = context;
            _matchhubContext = matchhubContext;
        }

        public async Task MatchCompleted(Match match)
        {
            await _matchhubContext.Clients.All.InvokeAsync("MatchCompleted", match);
        }

        public async Task Crawl(TeamColor teamColor)
        {
            await _matchhubContext.Clients.All.InvokeAsync("Crawl", teamColor);
        }
    }
}
