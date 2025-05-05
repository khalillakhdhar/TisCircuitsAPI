using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;
namespace TisCircuitsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class CoursController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;

    public CoursController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadCours([FromForm] IFormFile fichier, [FromForm] int formationId, [FromForm] string titre)
    {
        if (fichier == null || fichier.Length == 0)
            return BadRequest("Fichier invalide.");

        var formation = await _context.Formation.FindAsync(formationId);
        if (formation == null)
            return NotFound("Formation non trouvée.");

        var dossierPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "cours");
        if (!Directory.Exists(dossierPath))
            Directory.CreateDirectory(dossierPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(fichier.FileName);
        var fullPath = Path.Combine(dossierPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await fichier.CopyToAsync(stream);
        }

        var url = $"{Request.Scheme}://{Request.Host}/cours/{fileName}"; // 👈 URL absolue

        var cours = new Cours
        {
            Titre = titre,
            UrlFichier = url,
            FormationId = formationId
        };

        _context.Cours.Add(cours);
        await _context.SaveChangesAsync();

        return Ok(cours);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCours(int id, [FromBody] Cours updated)
    {
        var cours = await _context.Cours.FindAsync(id);
        if (cours == null)
            return NotFound();

        cours.Titre = updated.Titre;
        await _context.SaveChangesAsync();
        return Ok(cours);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCours(int id)
    {
        var cours = await _context.Cours.FindAsync(id);
        if (cours == null)
            return NotFound();

        var filePath = Path.Combine(_env.WebRootPath ?? "wwwroot", cours.UrlFichier.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        _context.Cours.Remove(cours);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("byFormation/{formationId}")]
    public async Task<IActionResult> GetCoursByFormation(int formationId)
    {
        var cours = await _context.Cours
            .Where(c => c.FormationId == formationId)
            .ToListAsync();

        return Ok(cours);
    }
}
