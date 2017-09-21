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
    [Route("api/Goals")]
    public class GoalsApiController : Controller
    {
        private readonly FoosballContext _context;

        public GoalsApiController(FoosballContext context)
        {
            _context = context;
        }

        // GET: api/GoalsApi
        [HttpGet]
        public IEnumerable<Goal> GetGoal()
        {
            return _context.Goal;
        }

        // GET: api/GoalsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goal = await _context.Goal.SingleOrDefaultAsync(m => m.Id == id);

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // PUT: api/GoalsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal([FromRoute] int id, [FromBody] Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goal.Id)
            {
                return BadRequest();
            }

            _context.Entry(goal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(id))
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

        // POST: api/GoalsApi
        [HttpPost]
        public async Task<IActionResult> PostGoal([FromBody] Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Goal.Add(goal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoal", new { id = goal.Id }, goal);
        }

        // DELETE: api/GoalsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goal = await _context.Goal.SingleOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Goal.Remove(goal);
            await _context.SaveChangesAsync();

            return Ok(goal);
        }

        private bool GoalExists(int id)
        {
            return _context.Goal.Any(e => e.Id == id);
        }
    }
}