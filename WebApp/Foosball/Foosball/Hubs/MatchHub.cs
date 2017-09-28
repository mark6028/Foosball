using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Foosball.Hubs
{
    public class MatchHub : Hub, IMatchHub
    {
        public Task MatchCompleted(string data)
        {
            return Clients.All.InvokeAsync("MatchCompleted", data);
        }

        public Task Crawl(string data)
        {
            return Clients.All.InvokeAsync("Crawl", data);
        }
    }
}
