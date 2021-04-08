using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreyToUsd.Data;
using AndreyToUsd.Models;

namespace AndreyToUsd.Controllers
{
    /// <summary>
    /// API is designed to control and update the ratios 
    /// of currencies to USD and to each other
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RateToUsdController : ControllerBase
    {
        private readonly ToUsdContext _context;

        

        public RateToUsdController(ToUsdContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of the of currencies to USD ratio 
        /// </summary>
        /// <returns></returns>
        // GET: api/RateToUsd
        [HttpGet]
        public async Task<ActionResult<RateToUsd[]>> GetRates()
        {
            return await _context.Rates.ToArrayAsync();
        }

        /// <summary>
        /// Get the ratio of currency to USD actual for the last hour
        /// </summary>
        /// <param name="code"> currency code - 3 letters for example EUR</param>
        /// <returns></returns>
        // GET: api/RateToUsd/5
        [HttpGet("{code}")]
        public async Task<ActionResult<RateToUsd>> GetRateToUsd(string code)
        {
            var rateToUsd = await _context.Rates.FindAsync(code);

            if (rateToUsd == null)
            {
                return NotFound();
            }

            return rateToUsd;
        }

           /// <summary>
        /// Get the ratio of two currencies From / To actual for the last hour
        /// </summary>
        /// <param name="from"> currency code - 3 letters for example EUR</param>
        /// <param name="to"> currency code - 3 letters for example JPY</param>
        /// <returns></returns>
        [HttpGet("{from}/{to}")]
        public async Task<ActionResult<FromTo>>
                GetFromToTo(string from,string to)
        {
            var rateToUsd = await _context.Rates.FindAsync(code);

            if (rateToUsd == null)
            {
                return NotFound();
            }

            return rateToUsd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="rateToUsd"></param>
        /// <returns></returns>
        // PUT: api/RateToUsd/RUB
        [HttpPut("{code}")]
        public async Task<IActionResult> PutRateToUsd(string code, RateToUsd rateToUsd)
        {
            if (code != rateToUsd.code)
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
                if (!RateToUsdExists(code))
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

        // DELETE: api/RateToUsd/RUB
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteRateToUsd(string RUB)
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
