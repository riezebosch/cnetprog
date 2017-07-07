using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeriesApi;
using SeriesApi.Model;

namespace SeriesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Seasons")]
    public class SeasonsController : Controller
    {
        private readonly SeriesContext _context;

        public SeasonsController(SeriesContext context)
        {
            _context = context;
        }

        //// GET: api/Seasons
        //[HttpGet]
        //public IEnumerable<Season> GetSeason()
        //{
        //    return _context.Season;
        //}

        // GET: api/Seasons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeason([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var season = await _context
                .Season
                .Select(s => new { s.Id, s.Year, Episodes = s.Episodes.Select(e => e.Id) })
                .SingleOrDefaultAsync(m => m.Id == id);

            if (season == null)
            {
                return NotFound();
            }

            return Ok(season);
        }

        // PUT: api/Seasons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeason([FromRoute] int id, [FromBody] Season season)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != season.Id)
            {
                return BadRequest();
            }

            _context.Entry(season).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeasonExists(id))
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

        // POST: api/Seasons
        [HttpPost]
        public async Task<IActionResult> PostSeason([FromBody] Season season)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Season.Add(season);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeason", new { id = season.Id }, season);
        }

        // DELETE: api/Seasons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var season = await _context.Season.SingleOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            _context.Season.Remove(season);
            await _context.SaveChangesAsync();

            return Ok(season);
        }

        private bool SeasonExists(int id)
        {
            return _context.Season.Any(e => e.Id == id);
        }
    }
}