using System.Threading.Tasks;

namespace Foosball.Hubs
{
    public interface IMatchHub
    {
        Task MatchCompleted(string data);
        Task Crawl(string data);
    }
}