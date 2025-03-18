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
    public class DemandeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DemandeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Demande
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Demande>>> GetDemande()
        {
            return await _context.Demande.ToListAsync();
        }

        // GET: api/Demande/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Demande>> GetDemande(int id)
        {
            var demande = await _context.Demande.FindAsync(id);

            if (demande == null)
            {
                return NotFound();
            }

            return demande;
        }

        // PUT: api/Demande/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemande(int id, Demande demande)
        {
            if (id != demande.id)
            {
                return BadRequest();
            }

            _context.Entry(demande).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandeExists(id))
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

        // POST: api/Demande
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Demande>> PostDemande(Demande demande)
        {
            _context.Demande.Add(demande);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDemande", new { id = demande.id }, demande);
        }

        // DELETE: api/Demande/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemande(int id)
        {
            var demande = await _context.Demande.FindAsync(id);
            if (demande == null)
            {
                return NotFound();
            }

            _context.Demande.Remove(demande);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DemandeExists(int id)
        {
            return _context.Demande.Any(e => e.id == id);
        }
    }
}
