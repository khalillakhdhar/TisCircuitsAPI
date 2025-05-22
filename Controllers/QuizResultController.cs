using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class QuizResultController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuizResultController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/QuizResult
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizResult>>> GetAllResults()
    {
        var results = await _context.QuizResults
            .Include(r => r.User)
            .Include(r => r.Quiz)
            .ToListAsync();

        if (results.Count == 0)
            return NotFound("Aucun résultat trouvé.");

        return results;
    }

    // GET: api/QuizResult/User/5
    [HttpGet("User/{userId}")]
    public async Task<ActionResult<IEnumerable<QuizResult>>> GetResultsByUser(int userId)
    {
        var results = await _context.QuizResults
            .Include(r => r.Quiz)
            .Where(r => r.UserId == userId)
            .ToListAsync();

        if (results.Count == 0)
            return NotFound("Aucun résultat trouvé pour cet utilisateur.");

        return results;
    }

    // GET: api/QuizResult/Quiz/5
    [HttpGet("Quiz/{quizId}")]
    public async Task<ActionResult<IEnumerable<QuizResult>>> GetResultsByQuiz(int quizId)
    {
        var results = await _context.QuizResults
            .Include(r => r.User)
            .Where(r => r.QuizId == quizId)
            .ToListAsync();

        if (results.Count == 0)
            return NotFound("Aucun résultat trouvé pour ce quiz.");

        return results;
    }

    // DELETE: api/QuizResult/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResult(int id)
    {
        var result = await _context.QuizResults.FindAsync(id);
        if (result == null)
            return NotFound();

        _context.QuizResults.Remove(result);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Statistique du nombre de personnes qui ont passé des quizzes
    [HttpGet("Stats/TotalParticipants")]
    public async Task<ActionResult<int>> GetTotalParticipants()
    {
        var totalParticipants = await _context.QuizResults
            .Select(r => r.UserId)
            .Distinct()
            .CountAsync();

        return totalParticipants;
    }

    // Statistique des personnes ayant obtenu plus de 10
    [HttpGet("Stats/PassedQuizAbove10")]
    public async Task<ActionResult<int>> GetPassedQuizAbove10()
    {
        var count = await _context.QuizResults
            .Where(r => r.score > 10)
            .Select(r => r.UserId)
            .Distinct()
            .CountAsync();

        return count;
    }

    // Statistique des personnes ayant obtenu moins de 10
    [HttpGet("Stats/FailedQuizBelow10")]
    public async Task<ActionResult<int>> GetFailedQuizBelow10()
    {
        var count = await _context.QuizResults
            .Where(r => r.score < 10)
            .Select(r => r.UserId)
            .Distinct()
            .CountAsync();

        return count;
    }
}
