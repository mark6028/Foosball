using System.Threading.Tasks;
using Foosball.Models;

namespace Foosball.Broadcasters
{
    public interface IMatchBroadcaster
    {
        Task Crawl(TeamColor teamcolor);
        Task MatchCompleted(Match match);
    }
}