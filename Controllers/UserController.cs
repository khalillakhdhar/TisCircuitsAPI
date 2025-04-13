using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace TisCircuitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUser()
        {
            var users = await _context.User
                .Include(u => u.role)
                .Include(u => u.Id_ServiceNavigation)
                .Select(u => new
                {
                    u.id,
                    u.nom_complet,
                    u.matricule,
                    u.matriculeWindows,
                    u.email,
                    u.role_id,
                    Role = u.role != null ? u.role.nom : null,
                    u.fonction,
                    u.responsable,
                    u.Id_Service,
                    u.Etats,
                    Service = u.Id_ServiceNavigation != null ? new
                    {
                        u.Id_ServiceNavigation.Id,
                        u.Id_ServiceNavigation.Nom,
                        u.Id_ServiceNavigation.Description
                    } : null
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUser(int id)
        {
            var user = await _context.User
                .Include(u => u.role)
                .Include(u => u.Id_ServiceNavigation)
                .Where(u => u.id == id)
                .Select(u => new
                {
                    u.id,
                    u.nom_complet,
                    u.matricule,
                    u.matriculeWindows,
                    u.email,
                    u.role_id,
                    Role = u.role != null ? u.role.nom : null,
                    u.fonction,
                    u.responsable,
                    u.Id_Service,
                    u.Etats,
                    Service = u.Id_ServiceNavigation != null ? new
                    {
                        u.Id_ServiceNavigation.Id,
                        u.Id_ServiceNavigation.Nom,
                        u.Id_ServiceNavigation.Description
                    } : null
                })
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
    }
}
