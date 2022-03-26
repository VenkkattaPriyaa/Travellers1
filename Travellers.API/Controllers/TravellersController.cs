using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travellers.API.Data;
using Travellers.API.Model;

namespace Travellers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravellersController : Controller
    {
        private readonly TravellersContext _context;

        public TravellersController(TravellersContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravellerValue>>> GetTravellerValue([FromQuery] string name)
        {
            var Travellerquery = from h in _context.TravellerValue select h;

           
            if (!String.IsNullOrEmpty(name))
            {
                Travellerquery = Travellerquery.Where(
                    h => h.name.Contains(name));
            }
            
            return await Travellerquery.OrderBy(num => num.id).ToListAsync();
        }

        
        [HttpGet("{travellerid}")]
        public async Task<ActionResult<TravellerValue>> GetTravellerValue(int id)
        {
            var TravellerValue = await _context.TravellerValue.FindAsync(id);

            if (TravellerValue == null)
            {
                return NotFound();
            }

            return TravellerValue;
        }

       
        
        [HttpPut("{travellerid}")]
        public async Task<IActionResult> PutTravellerValue(int id, TravellerValue TravellerValue)
        {
            if (id != TravellerValue.id)
            {
                return BadRequest();
            }

            _context.Entry(TravellerValue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravellerValueExists(id))
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
[       HttpPost]
        public async Task<ActionResult<TravellerValue>> PostTravellerValue(TravellerValue TravellerValue)
        {
            
            if (TravellerValue == null)
            {
                return BadRequest();
            }
            
            TravellerValue.id = _context.TravellerValue.Max(h => h.id) + 1;
            _context.TravellerValue.Add(TravellerValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTravellerValue", new { id = TravellerValue.travellerid }, TravellerValue);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<TravellerValue>> DeleteTravellerValue(int id)
        {
            var TravellerValue = await _context.TravellerValue.FindAsync(id);
            if (TravellerValue == null)
            {
                return NotFound();
            }

            _context.TravellerValue.Remove(TravellerValue);
            await _context.SaveChangesAsync();

            return TravellerValue;
        }

        private bool TravellerValueExists(int id)
        {
            return _context.TravellerValue.Any(e => e.travellerid == id);
        }
    }
}
