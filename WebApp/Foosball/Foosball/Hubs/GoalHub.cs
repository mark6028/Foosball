using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Foosball.Hubs
{
    public class GoalHub : Hub, IGoalHub
    {
        public Task GoalScored(string data)
        {
            return Clients.All.InvokeAsync("GoalScored", data);
        }
    }
}
