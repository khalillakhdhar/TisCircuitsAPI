using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TisCircuitsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 🔒 Windows Auth nécessite authentification
    [Authorize]
    [HttpGet("windows")]
    public async Task<IActionResult> AuthWindows()
    {
        var login = HttpContext.User.Identity?.Name;

        if (string.IsNullOrEmpty(login))
            return Unauthorized("Identifiant Windows non reconnu");

        var user = await _context.User
            .Include(u => u.role)
            .Include(u => u.Id_ServiceNavigation)
            .FirstOrDefaultAsync(u => u.matriculeWindows == login);

        if (user == null)
            return Unauthorized("Aucun utilisateur correspondant au compte Windows");

        return Ok(MapUser(user));
    }

    // ✅ Auth via matricule — public
    [AllowAnonymous]
    [HttpPost("matricule")]
    public async Task<IActionResult> AuthMatricule([FromBody] MatriculeLoginRequest req)
    {
        var user = await _context.User
            .Include(u => u.role)
            .Include(u => u.Id_ServiceNavigation)
            .FirstOrDefaultAsync(u => u.matricule == req.matricule);

        if (user == null)
            return Unauthorized("Matricule invalide");

        return Ok(MapUser(user));
    }

    private object MapUser(User user)
    {
        return new
        {
            user.id,
            user.nom_complet,
            user.matricule,
            user.matriculeWindows,
            user.email,
            user.fonction,
            user.responsable,
            user.Etats,
            Role = user.role?.nom,
            Service = user.Id_ServiceNavigation != null ? new
            {
                user.Id_ServiceNavigation.Id,
                user.Id_ServiceNavigation.Nom,
                user.Id_ServiceNavigation.Description,
                user.Id_ServiceNavigation.Date
            } : null
        };
    }

    public class MatriculeLoginRequest
    {
        public string matricule { get; set; } = null!;
    }
}
