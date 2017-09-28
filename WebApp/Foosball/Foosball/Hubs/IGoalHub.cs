using System.Threading.Tasks;

namespace Foosball.Hubs
{
    public interface IGoalHub
    {
        Task GoalScored(string data);
    }
}