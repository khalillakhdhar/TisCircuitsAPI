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
    public class TypeFournitureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TypeFournitureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TypeFourniture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type_Fourniture>>> GetType_Fourniture()
        {
            return await _context.Type_Fourniture.ToListAsync();
        }

        // GET: api/TypeFourniture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type_Fourniture>> GetType_Fourniture(int id)
        {
            var type_Fourniture = await _context.Type_Fourniture.FindAsync(id);

            if (type_Fourniture == null)
            {
                return NotFound();
            }

            return type_Fourniture;
        }

        // PUT: api/TypeFourniture/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType_Fourniture(int id, Type_Fourniture type_Fourniture)
        {
            if (id != type_Fourniture.id)
            {
                return BadRequest();
            }

            _context.Entry(type_Fourniture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Type_FournitureExists(id))
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

        // POST: api/TypeFourniture
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Type_Fourniture>> PostType_Fourniture(Type_Fourniture type_Fourniture)
        {
            _context.Type_Fourniture.Add(type_Fourniture);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Type_FournitureExists(type_Fourniture.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetType_Fourniture", new { id = type_Fourniture.id }, type_Fourniture);
        }

        // DELETE: api/TypeFourniture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType_Fourniture(int id)
        {
            var type_Fourniture = await _context.Type_Fourniture.FindAsync(id);
            if (type_Fourniture == null)
            {
                return NotFound();
            }

            _context.Type_Fourniture.Remove(type_Fourniture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Type_FournitureExists(int id)
        {
            return _context.Type_Fourniture.Any(e => e.id == id);
        }
    }
}
