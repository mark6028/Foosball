using System.Threading.Tasks;
using Foosball.Models;

namespace Foosball.Broadcasters
{
    public interface IGoalBroadcaster
    {
        Task BroadcastGoalCreated(Goal goal);
    }
}