using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;

namespace TisCircuitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournitureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FournitureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fourniture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fourniture>>> GetFourniture()
        {
            return await _context.Fourniture.ToListAsync();
        }

        // GET: api/Fourniture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fourniture>> GetFourniture(int id)
        {
            var fourniture = await _context.Fourniture.FindAsync(id);

            if (fourniture == null)
            {
                return NotFound();
            }

            return fourniture;
        }

        // PUT: api/Fourniture/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFourniture(int id, Fourniture fourniture)
        {
            if (id != fourniture.id)
            {
                return BadRequest();
            }

            _context.Entry(fourniture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FournitureExists(id))
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

        // POST: api/Fourniture
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fourniture>> PostFourniture(Fourniture fourniture)
        {
            _context.Fourniture.Add(fourniture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFourniture", new { id = fourniture.id }, fourniture);
        }

        // DELETE: api/Fourniture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFourniture(int id)
        {
            var fourniture = await _context.Fourniture.FindAsync(id);
            if (fourniture == null)
            {
                return NotFound();
            }

            _context.Fourniture.Remove(fourniture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FournitureExists(int id)
        {
            return _context.Fourniture.Any(e => e.id == id);
        }
    }
}
