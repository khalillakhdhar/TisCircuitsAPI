using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TisCircuitsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class DemandeCongeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DemandeCongeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DemandeConge>>> GetDemandes()
    {
        return await _context.DemandeConges.Include(d => d.User).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DemandeConge>> GetDemande(int id)
    {
        var demande = await _context.DemandeConges.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);
        return demande == null ? NotFound() : demande;
    }

    [HttpPost]
    public async Task<ActionResult<DemandeConge>> PostDemande(DemandeConge demande)
    {
        _context.DemandeConges.Add(demande);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDemande), new { id = demande.Id }, demande);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDemande(int id, DemandeConge demande)
    {
        if (id != demande.Id) return BadRequest();

        _context.Entry(demande).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDemande(int id)
    {
        var demande = await _context.DemandeConges.FindAsync(id);
        if (demande == null) return NotFound();

        _context.DemandeConges.Remove(demande);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
