using System.Threading.Tasks;

namespace Foosball.Hubs
{
    public interface IGoalHub
    {
        Task Send(string data);
    }
}