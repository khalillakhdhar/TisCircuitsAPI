using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]

public class QuizQuestionController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public QuizQuestionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/QuizQuestion/Quiz/5
    [HttpGet("quiz/{quizId}")]
    public async Task<ActionResult<IEnumerable<QuizQuestion>>> GetQuestionsByQuiz(int quizId)
    {
        var questions = await _context.QuizQuestions
            .Include(q => q.Options)
            .Where(q => q.QuizId == quizId)
            .ToListAsync();

        return Ok(questions);
    }

    // GET: api/QuizQuestion/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizQuestion>> GetQuestion(int id)
    {
        var question = await _context.QuizQuestions
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.id == id);

        if (question == null) return NotFound();

        return Ok(question);
    }

    // POST: api/QuizQuestion
    [HttpPost]
    public async Task<ActionResult<QuizQuestion>> CreateQuestion(QuizQuestion question)
    {
        _context.QuizQuestions.Add(question);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuestion), new { id = question.id }, question);
    }

    // PUT: api/QuizQuestion/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(int id, QuizQuestion question)
    {
        if (id != question.id) return BadRequest();

        _context.Entry(question).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuizQuestionExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/QuizQuestion/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.QuizQuestions.FindAsync(id);
        if (question == null) return NotFound();

        _context.QuizQuestions.Remove(question);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool QuizQuestionExists(int id) =>
        _context.QuizQuestions.Any(q => q.id == id);
}
