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
    public class GoalsApiController : BaseApiController<Goal, GoalDTO>
    {
        private readonly IGoalBroadcaster _goalBroadcaster;

        public GoalsApiController(FoosballContext context, IGoalBroadcaster goalBroadcaster)
            :base(context)
        {
            _dbEntity = _context.Goal;
            _goalBroadcaster = goalBroadcaster;
        }

        [HttpPost]
        public override IActionResult Insert(GoalDTO entity)
        {
            if (!TryValidateModel(entity))
                return BadRequest(ModelState.ToString());

            var model = new Goal();
            ConvertDTOToModel(entity, out model);

            _dbEntity.Add(model);
            _context.SaveChanges();

            //broadcast goal scored to all signalR clients
            _goalBroadcaster.GoalScored(model);

            return Ok();
        }
    }
}