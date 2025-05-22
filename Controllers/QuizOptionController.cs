using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]

public class QuizOptionController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public QuizOptionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/QuizOption/Question/5
    [HttpGet("question/{questionId}")]
    public async Task<ActionResult<IEnumerable<QuizOption>>> GetOptionsByQuestion(int questionId)
    {
        var options = await _context.QuizOptions
            .Where(o => o.QuizQuestionId == questionId)
            .ToListAsync();

        return Ok(options);
    }

    // GET: api/QuizOption/5
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizOption>> GetOption(int id)
    {
        var option = await _context.QuizOptions.FindAsync(id);
        if (option == null) return NotFound();

        return Ok(option);
    }

    // POST: api/QuizOption
    [HttpPost]
    public async Task<ActionResult<QuizOption>> CreateOption(QuizOption option)
    {
        _context.QuizOptions.Add(option);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOption), new { id = option.id }, option);
    }

    // PUT: api/QuizOption/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOption(int id, QuizOption option)
    {
        if (id != option.id) return BadRequest();

        _context.Entry(option).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            if (!QuizOptionExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }

    // DELETE: api/QuizOption/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOption(int id)
    {
        var option = await _context.QuizOptions.FindAsync(id);
        if (option == null) return NotFound();

        _context.QuizOptions.Remove(option);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool QuizOptionExists(int id) =>
        _context.QuizOptions.Any(o => o.id == id);
}
