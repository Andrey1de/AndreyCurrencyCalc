using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreyToUsd.Data;
using AndreyToUsd.Models;

namespace AndreyToUsd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateToUsdController : ControllerBase
    {
        private readonly ToUsdContext _context;

        public RateToUsdController(ToUsdContext context)
        {
            _context = context;
        }

        // GET: api/RateToUsd
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateToUsd>>> GetRates()
        {
            return await _context.Rates.ToListAsync();
        }

        // GET: api/RateToUsd/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RateToUsd>> GetRateToUsd(string id)
        {
            var rateToUsd = await _context.Rates.FindAsync(id);

            if (rateToUsd == null)
            {
                return NotFound();
            }

            return rateToUsd;
        }

        // PUT: api/RateToUsd/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRateToUsd(string id, RateToUsd rateToUsd)
        {
            if (id != rateToUsd.code)
            {
                return BadRequest();
            }

            _context.Entry(rateToUsd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateToUsdExists(id))
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

        // POST: api/RateToUsd
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RateToUsd>> PostRateToUsd(RateToUsd rateToUsd)
        {
            _context.Rates.Add(rateToUsd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RateToUsdExists(rateToUsd.code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRateToUsd", new { id = rateToUsd.code }, rateToUsd);
        }

        // DELETE: api/RateToUsd/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateToUsd(string id)
        {
            var rateToUsd = await _context.Rates.FindAsync(id);
            if (rateToUsd == null)
            {
                return NotFound();
            }

            _context.Rates.Remove(rateToUsd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RateToUsdExists(string id)
        {
            return _context.Rates.Any(e => e.code == id);
        }
    }
}
