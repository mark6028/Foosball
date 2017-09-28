using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;
using Foosball.Broadcasters;
using Newtonsoft.Json;

namespace Foosball.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Matches")]
    public class MatchesApiController : BaseApiController<Match, MatchDTO>
    {
        private readonly IMatchBroadcaster _matchBroadcaster;

        public MatchesApiController(FoosballContext context, IMatchBroadcaster matchBroadcaster)
            : base(context)
        {
            _dbEntity = _context.Match;
            _matchBroadcaster = matchBroadcaster;
        }

        protected override IActionResult HandleUpdate(Match model)
        {
            if (!TryValidateModel(model))
                return BadRequest();

            try
            {
                _context.SaveChanges();

                if (model.State == MatchState.Completed)
                {
                    //broadcast match completed to all signalR clients
                    _matchBroadcaster.MatchCompleted(model);
                    CheckCrawlConditions(model.Id);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }        

        private void CheckCrawlConditions(int matchId)
        {
            var matchGoals = _context.Goal
                .Include(g => g.Match)
                .AsNoTracking()
                .Where(g => g.MatchId == matchId)
                .Select(g => new Goal
                {
                    TeamColor = g.TeamColor,
                    MatchId = g.MatchId
                });

            var blackScored = false;
            var greyScored = false;
            foreach (var goal in matchGoals)
            {
                if (blackScored && greyScored)
                    break;

                if (!blackScored && goal.TeamColor == TeamColor.Black)
                    blackScored = true;

                if (!greyScored && goal.TeamColor == TeamColor.Grey)
                    greyScored = true;
            }

            if (blackScored && !greyScored)
            {
                _matchBroadcaster.Crawl(TeamColor.Black);
            }

            if (!blackScored && greyScored)
            {
                _matchBroadcaster.Crawl(TeamColor.Grey);
            }
        }
    }
}