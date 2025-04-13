using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TisCircuitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
            return await _context.Fourniture
                .Include(f => f.Type_Fourniture)
                .Include(f => f.Matiere)
                .ToListAsync();
        }

        // GET: api/Fourniture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fourniture>> GetFourniture(int id)
        {
            var item = await _context.Fourniture
                .Include(f => f.Type_Fourniture)
                .Include(f => f.Matiere)
                .FirstOrDefaultAsync(f => f.id == id);

            return item == null ? NotFound() : item;
        }

        // POST: api/Fourniture
        [HttpPost]
        public async Task<ActionResult<Fourniture>> PostFourniture(Fourniture f)
        {
            _context.Fourniture.Add(f);

            if (f.etats == "Validée RH" && f.MatiereId != null)
            {
                var matiere = await _context.Matiere.FindAsync(f.MatiereId);
                if (matiere != null && matiere.Quantite >= (f.quantite ?? 0))
                {
                    matiere.Quantite -= f.quantite ?? 0;
                }
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFourniture", new { id = f.id }, f);
        }

        // PUT: api/Fourniture/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFourniture(int id, Fourniture f)
        {
            if (id != f.id)
                return BadRequest();

            _context.Entry(f).State = EntityState.Modified;

            if (f.etats == "Validée RH" && f.MatiereId != null)
            {
                var matiere = await _context.Matiere.FindAsync(f.MatiereId);
                if (matiere != null && matiere.Quantite >= (f.quantite ?? 0))
                {
                    matiere.Quantite -= f.quantite ?? 0;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Fourniture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFourniture(int id)
        {
            var item = await _context.Fourniture.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.Fourniture.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
