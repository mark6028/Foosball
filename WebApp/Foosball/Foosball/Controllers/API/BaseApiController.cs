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
    public abstract class BaseApiController<T, TDTO> : Controller
        where TDTO : Entity, new()
        where T : TDTO, new()
    {
        protected readonly FoosballContext _context;
        protected DbSet<T> _dbEntity;

        public BaseApiController(FoosballContext context)
        {
            _context = context;
        }

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

            var dto = new TDTO();
            ConvertModelToDTO(entity, out dto);

            return Ok(dto);
        }

        [HttpPost]
        [Route("json")]
        public virtual IActionResult InsertJson([FromForm] string values)
        {
            var entity = new T();
            JsonConvert.PopulateObject(values, entity);

            return Insert(entity);
        }

        [HttpPost]
        public virtual IActionResult Insert(TDTO entity)
        {
            if (!TryValidateModel(entity))
                return BadRequest(ModelState.ToString());

            var model = new T();
            ConvertDTOToModel(entity, out model);

            _dbEntity.Add(model);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("json")]
        public virtual IActionResult UpdateJson([FromForm] int key, [FromForm] string values)
        {
            var entity = _dbEntity.First(o => o.Id == key);
            JsonConvert.PopulateObject(values, entity);

            return HandleUpdate(entity);
        }

        [HttpPut]
        public virtual IActionResult Update(TDTO entity)
        {
            var model = new T();
            ConvertDTOToModel(entity, out model);

            _context.Entry(model).State = EntityState.Modified;

            return HandleUpdate(model);
        }

        private IActionResult HandleUpdate(T model)
        {
            if (!TryValidateModel(model))
                return BadRequest();

            try
            {
                _context.SaveChanges();
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

        protected void ConvertDTOToModel(TDTO source, out T model)
        {
            model = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        protected void ConvertModelToDTO(T source, out TDTO dto)
        {
            dto = JsonConvert.DeserializeObject<TDTO>(JsonConvert.SerializeObject(source));
        }
    }
}