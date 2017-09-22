using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Foosball.Hubs
{
    public class GoalHub : Hub, IGoalHub
    {
        public Task Send(string data)
        {
            return Clients.All.InvokeAsync("Send", data);
        }
    }
}
