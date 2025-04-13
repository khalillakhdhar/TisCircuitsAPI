using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TisCircuitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccesFormationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccesFormationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccesFormation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccesFormation>>> GetAccesFormation()
        {
            return await _context.AccesFormation.ToListAsync();
        }

        // GET: api/AccesFormation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccesFormation>> GetAccesFormation(int id)
        {
            var accesFormation = await _context.AccesFormation.FindAsync(id);

            if (accesFormation == null)
            {
                return NotFound();
            }

            return accesFormation;
        }

        // PUT: api/AccesFormation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccesFormation(int id, AccesFormation accesFormation)
        {
            if (id != accesFormation.Id)
            {
                return BadRequest();
            }

            _context.Entry(accesFormation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccesFormationExists(id))
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

        // POST: api/AccesFormation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccesFormation>> PostAccesFormation(AccesFormation accesFormation)
        {
            _context.AccesFormation.Add(accesFormation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccesFormation", new { id = accesFormation.Id }, accesFormation);
        }

        // DELETE: api/AccesFormation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccesFormation(int id)
        {
            var accesFormation = await _context.AccesFormation.FindAsync(id);
            if (accesFormation == null)
            {
                return NotFound();
            }

            _context.AccesFormation.Remove(accesFormation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccesFormationExists(int id)
        {
            return _context.AccesFormation.Any(e => e.Id == id);
        }
    }
}
