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
    public class FormationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Formation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formation>>> GetFormation()
        {
            return await _context.Formation.ToListAsync();
        }

        // GET: api/Formation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Formation>> GetFormation(int id)
        {
            var formation = await _context.Formation.FindAsync(id);

            if (formation == null)
            {
                return NotFound();
            }

            return formation;
        }

        // PUT: api/Formation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormation(int id, Formation formation)
        {
            if (id != formation.id)
            {
                return BadRequest();
            }

            _context.Entry(formation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormationExists(id))
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

        // POST: api/Formation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Formation>> PostFormation(Formation formation)
        {
            _context.Formation.Add(formation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormation", new { id = formation.id }, formation);
        }

        // DELETE: api/Formation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormation(int id)
        {
            var formation = await _context.Formation.FindAsync(id);
            if (formation == null)
            {
                return NotFound();
            }

            _context.Formation.Remove(formation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormationExists(int id)
        {
            return _context.Formation.Any(e => e.id == id);
        }
    }
}
