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
    public class DetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Details
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Details>>> GetDetails()
        {
            return await _context.Details.ToListAsync();
        }

        // GET: api/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Details>> GetDetails(int id)
        {
            var details = await _context.Details.FindAsync(id);

            if (details == null)
            {
                return NotFound();
            }

            return details;
        }

        // PUT: api/Details/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetails(int id, Details details)
        {
            if (id != details.id)
            {
                return BadRequest();
            }

            _context.Entry(details).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailsExists(id))
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

        // POST: api/Details
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Details>> PostDetails(Details details)
        {
            _context.Details.Add(details);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetailsExists(details.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetails", new { id = details.id }, details);
        }

        // DELETE: api/Details/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetails(int id)
        {
            var details = await _context.Details.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }

            _context.Details.Remove(details);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetailsExists(int id)
        {
            return _context.Details.Any(e => e.id == id);
        }
    }
}
