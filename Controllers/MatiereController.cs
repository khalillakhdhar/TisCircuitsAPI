using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TisCircuitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MatiereController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MatiereController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatiere()
            => await _context.Matiere.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            var matiere = await _context.Matiere.FindAsync(id);
            if (matiere == null) return NotFound();
            return matiere;
        }

        [HttpPost]
        public async Task<ActionResult<Matiere>> PostMatiere([FromBody] Matiere matiere)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Matiere.Add(matiere);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatiere), new { id = matiere.Id }, matiere);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatiere(int id, [FromBody] Matiere matiere)
        {
            if (id != matiere.Id) return BadRequest();
            _context.Entry(matiere).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Matiere.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatiere(int id)
        {
            var matiere = await _context.Matiere.FindAsync(id);
            if (matiere == null)
                return NotFound();

            bool isUsed = await _context.Fourniture.AnyAsync(f => f.MatiereId == id);
            if (isUsed)
                return BadRequest("Impossible de supprimer : cette matière est liée à une fourniture.");

            _context.Matiere.Remove(matiere);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
