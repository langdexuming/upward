using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reggie.Upward.WebApi.Areas.Car.Data;
using Reggie.Upward.WebApi.Areas.Car.Models;

namespace Reggie.Upward.WebApi.Areas.Car.Controllers
{
    [Area("Car")]
    [Produces("application/json")]
    [Route("api/[Area]/[controller]")]
    public class SeriesController : Controller
    {
        private readonly CarContext _context;

        public SeriesController(CarContext context)
        {
            _context = context;
        }

        // GET: api/Series
        [HttpGet]
        public IEnumerable<Series> GetSerieses()
        {
            return _context.Serieses;
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeries([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var series = await _context.Serieses.SingleOrDefaultAsync(m => m.SeriesId == id);

            if (series == null)
            {
                return NotFound();
            }

            return Ok(series);
        }

        // PUT: api/Series/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeries([FromRoute] int id, [FromBody] Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != series.SeriesId)
            {
                return BadRequest();
            }

            _context.Entry(series).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeriesExists(id))
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

        // POST: api/Series
        [HttpPost]
        public async Task<IActionResult> PostSeries([FromBody] Series series)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Serieses.Add(series);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeries", new { id = series.SeriesId }, series);
        }

        // DELETE: api/Series/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var series = await _context.Serieses.SingleOrDefaultAsync(m => m.SeriesId == id);
            if (series == null)
            {
                return NotFound();
            }

            _context.Serieses.Remove(series);
            await _context.SaveChangesAsync();

            return Ok(series);
        }

        private bool SeriesExists(int id)
        {
            return _context.Serieses.Any(e => e.SeriesId == id);
        }
    }
}