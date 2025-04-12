// Le contrôleur QuizController gère les opérations CRUD pour les quiz, telles que la récupération, la création, la mise à jour et la suppression de quiz

using ApiNORDev.Dto;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public QuizController(ApiNORDevContext context)
        {
            _context = context;
        }

        // pour récupérer un quiz spécifique par son identifiant
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer un quiz par ID",
            Description = "Récupère un quiz spécifique avec toutes ses questions et options."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Quiz trouvé", typeof(QuizDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Quiz introuvable")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
            var quiz = await _context
                .Quizzes.Where(q => q.Id == id)
                .Select(q => new
                {
                    q.Id,
                    q.Titre,
                    QuestionsQuiz = q
                        .QuestionsQuiz.Select(qq => new
                        {
                            qq.Id,
                            qq.Question,
                            qq.Explication,
                            qq.QuizId,
                            Options = qq
                                .Options.Select(o => new
                                {
                                    o.Id,
                                    o.Texte,
                                    o.EstCorrecte,
                                    o.QuestionQuizId,
                                })
                                .ToList(),
                        })
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // pour récupérer tous les quiz disponibles
        [HttpGet]
        [SwaggerOperation(
            Summary = "Récupérer tous les quiz",
            Description = "Liste complète des quiz avec leurs questions et options."
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des quiz récupérée",
            typeof(List<QuizDTO>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucun quiz trouvé")]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetAllQuizzes()
        {
            var quizzes = await _context
                .Quizzes.Include(q => q.QuestionsQuiz)
                .ThenInclude(q => q.Options)
                .ToListAsync();

            if (!quizzes.Any())
                return NotFound("Aucun quiz trouvé.");

            return Ok(quizzes.Select(q => new QuizDTO(q)).ToList());
        }

        public class QuizInput
        {
            public string Titre { get; set; } = null!;
            public List<int> QuestionIds { get; set; } = new();
        }

        // pour créer un nouveau quiz
        [HttpPost]
        [SwaggerOperation(Summary = "Créer un nouveau quiz")]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizInput input)
        {
            var questions = await _context
                .QuestionsQuiz.Include(q => q.Options)
                .Where(q => input.QuestionIds.Contains(q.Id))
                .ToListAsync();

            if (questions.Count != input.QuestionIds.Count)
                return BadRequest("Une ou plusieurs questions n'existent pas.");

            var quiz = new Quiz { Titre = input.Titre, QuestionsQuiz = questions };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            var result = new
            {
                quiz.Id,
                quiz.Titre,
                QuestionsQuiz = questions.Select(q => new
                {
                    q.Id,
                    q.Question,
                    q.Explication,
                    q.QuizId,
                    Options = q.Options.Select(o => new
                    {
                        o.Id,
                        o.Texte,
                        o.EstCorrecte,
                        o.QuestionQuizId,
                    }),
                }),
            };

            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, result);
        }

        // pour modifier un quiz existant
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier un quiz existant")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizInput input)
        {
            var quiz = await _context
                .Quizzes.Include(q => q.QuestionsQuiz)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
                return NotFound($"Quiz ID {id} non trouvé.");

            var questions = await _context
                .QuestionsQuiz.Include(q => q.Options)
                .Where(q => input.QuestionIds.Contains(q.Id))
                .ToListAsync();

            if (questions.Count != input.QuestionIds.Count)
                return BadRequest("Une ou plusieurs questions n'existent pas.");

            quiz.Titre = input.Titre;
            quiz.QuestionsQuiz = questions;

            await _context.SaveChangesAsync();

            var result = new
            {
                quiz.Id,
                quiz.Titre,
                QuestionsQuiz = questions.Select(q => new
                {
                    q.Id,
                    q.Question,
                    q.Explication,
                    q.QuizId,
                    Options = q.Options.Select(o => new
                    {
                        o.Id,
                        o.Texte,
                        o.EstCorrecte,
                        o.QuestionQuizId,
                    }),
                }),
            };

            return Ok(result);
        }

        // pour supprimer un quiz par son identifiant
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer un quiz",
            Description = "Supprime un quiz ainsi que toutes ses questions et options associées."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Quiz supprimé avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Quiz introuvable")]
        public async Task<ActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context
                .Quizzes.Include(q => q.QuestionsQuiz)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
                return NotFound($"Le quiz avec l'ID {id} n'existe pas.");

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Quiz avec l'ID {id} supprimé avec succès." });
        }
    }
}
