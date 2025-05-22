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
    public class DemandeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DemandeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Demande>>> GetDemande() =>
            await _context.Demande.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Demande>> GetDemande(int id)
        {
            var demande = await _context.Demande.FindAsync(id);
            return demande == null ? NotFound() : demande;
        }

        [HttpPost]
        public async Task<ActionResult<Demande>> PostDemande(Demande demande)
        {
            _context.Demande.Add(demande);

            if (demande.etat == "Validée RH" && demande.id_fichier != null)
            {
                var fichier = await _context.DemandeEmp.FindAsync(demande.id_fichier);
                var matiere = await _context.Matiere.FirstOrDefaultAsync(m => m.Nom == fichier.nom);

                if (matiere != null && matiere.Quantite > 0)
                {
                    matiere.Quantite -= 1;
                }
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDemande", new { id = demande.id }, demande);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemande(int id, Demande demande)
        {
            if (id != demande.id) return BadRequest();
            _context.Entry(demande).State = EntityState.Modified;

            if (demande.etat == "Validée RH" && demande.id_fichier != null)
            {
                var fichier = await _context.DemandeEmp.FindAsync(demande.id_fichier);
                var matiere = await _context.Matiere.FirstOrDefaultAsync(m => m.Nom == fichier.nom);

                if (matiere != null && matiere.Quantite > 0)
                {
                    matiere.Quantite -= 1;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemande(int id)
        {
            var item = await _context.Demande.FindAsync(id);
            if (item == null) return NotFound();

            _context.Demande.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}