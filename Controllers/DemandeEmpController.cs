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
    public class DemandeEmpController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DemandeEmpController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DemandeEmp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandeEmp>>> GetDemandeEmp()
        {
            return await _context.DemandeEmp.ToListAsync();
        }

        // GET: api/DemandeEmp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DemandeEmp>> GetDemandeEmp(int id)
        {
            var demandeEmp = await _context.DemandeEmp.FindAsync(id);

            if (demandeEmp == null)
            {
                return NotFound();
            }

            return demandeEmp;
        }

        // PUT: api/DemandeEmp/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemandeEmp(int id, DemandeEmp demandeEmp)
        {
            if (id != demandeEmp.id)
            {
                return BadRequest();
            }

            _context.Entry(demandeEmp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandeEmpExists(id))
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

        // POST: api/DemandeEmp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DemandeEmp>> PostDemandeEmp(DemandeEmp demandeEmp)
        {
            _context.DemandeEmp.Add(demandeEmp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDemandeEmp", new { id = demandeEmp.id }, demandeEmp);
        }

        // DELETE: api/DemandeEmp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemandeEmp(int id)
        {
            var demandeEmp = await _context.DemandeEmp.FindAsync(id);
            if (demandeEmp == null)
            {
                return NotFound();
            }

            _context.DemandeEmp.Remove(demandeEmp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DemandeEmpExists(int id)
        {
            return _context.DemandeEmp.Any(e => e.id == id);
        }
    }
}
