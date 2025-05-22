using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TisCircuitsAPI.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]

public class QuizController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public QuizController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET api/quiz/formation/5
    [HttpGet("formation/{formationId}")]
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes(int formationId)
    {
        var quizzes = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(qt => qt.Options)
            .Where(q => q.FormationId == formationId)
            .ToListAsync();

        return Ok(quizzes);
    }

    // POST api/quiz
    [HttpPost]
    public async Task<ActionResult<Quiz>> CreateQuiz(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuizzes), new { formationId = quiz.FormationId }, quiz);
    }

    // POST api/quiz/{quizId}/submit/{userId}
    // Body JSON: { "answers": [ { "questionId":1, "optionId":3 }, { "questionId":2, "optionId":5 } ] }
    [HttpPost("{quizId}/submit/{userId}")]
    public async Task<ActionResult> SubmitQuizResult(int quizId, int userId, [FromBody] QuizSubmission submission)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(qt => qt.Options)
            .FirstOrDefaultAsync(q => q.id == quizId);
        if (quiz == null) return NotFound("Quiz not found");

        int score = 0;
        int pointsPerQuestion = 20 / quiz.Questions.Count;

        foreach (var q in quiz.Questions)
        {
            var answer = submission.Answers.FirstOrDefault(a => a.QuestionId == q.id);
            if (answer != null)
            {
                var option = q.Options.FirstOrDefault(o => o.id == answer.OptionId);
                if (option != null && option.is_correct)
                {
                    score += pointsPerQuestion;
                }
            }
        }

        var result = new QuizResult
        {
            QuizId = quizId,
            UserId = userId,
            score = score,
            date_taken = DateTime.UtcNow
        };
        _context.QuizResults.Add(result);
        await _context.SaveChangesAsync();

        return Ok(new { score });
    }
}

// Classe pour la soumission des réponses (dans ce même fichier ou séparé)
public class QuizSubmission
{
    public List<Answer> Answers { get; set; } = new List<Answer>();
}

public class Answer
{
    public int QuestionId { get; set; }
    public int OptionId { get; set; }
}
