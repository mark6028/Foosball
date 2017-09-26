using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Foosball.Broadcasters;

namespace Foosball.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Goals")]
    public class GoalsApiController : BaseApiController<Goal>
    {
        private readonly IGoalBroadcaster _goalBroadcaster;

        public GoalsApiController(FoosballContext context, IGoalBroadcaster goalBroadcaster)
            :base(context)
        {
            _dbEntity = _context.Goal;
            _goalBroadcaster = goalBroadcaster;
        }

        [HttpPost]
        public override IActionResult Insert([FromForm] string values)
        {
            var entity = new Goal();
            JsonConvert.PopulateObject(values, entity);

            if (!TryValidateModel(entity))
                return BadRequest(ModelState.ToString());

            _dbEntity.Add(entity);
            _context.SaveChanges();

            //broadcast goal scored to all signalR clients
            _goalBroadcaster.GoalScored(entity);

            return Ok();
        }
    }
}