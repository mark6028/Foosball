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
using Microsoft.AspNetCore.Authorization;

namespace Foosball.Controllers.API
{
    [Produces("application/json")]
    [Authorize]
    public abstract class BaseApiController<T> : Controller where T: Entity, new()
    {
        protected readonly FoosballContext _context;
        protected DbSet<T> _dbEntity;

        public BaseApiController(FoosballContext context)
        {
            _context = context;
        }

        //--start devextreme API--

        [HttpGet]
        public virtual object Get(WebApiDataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_dbEntity, loadOptions);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _dbEntity.SingleOrDefaultAsync(m => m.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public virtual IActionResult Insert([FromForm] string values)
        {
            var entity = new T();
            JsonConvert.PopulateObject(values, entity);

            if (!TryValidateModel(entity))
                return BadRequest(ModelState.ToString());

            _dbEntity.Add(entity);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public virtual IActionResult Update([FromForm] int key, [FromForm] string values)
        {
            var entity = _dbEntity.First(o => o.Id == key);
            JsonConvert.PopulateObject(values, entity);

            if (!TryValidateModel(entity))
                return BadRequest(ModelState.ToString());

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(entity.Id))
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

        [HttpDelete]
        public virtual void Delete([FromForm] int key)
        {
            var entity = _dbEntity.First(o => o.Id == key);
            _dbEntity.Remove(entity);
            _context.SaveChanges();
        }

        private bool EntityExists(int id)
        {
            return _dbEntity.Any(e => e.Id == id);
        }
    }
}