using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foosball.Models;

namespace Foosball.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Players")]
    public class PlayersApiController : BaseApiController<Player>
    {
        public PlayersApiController(FoosballContext context)
            : base(context)
        {
            _dbEntity = _context.Player;
        }
    }
}