using ApiNORDev.Data;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionQuizController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public QuestionQuizController(ApiNORDevContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste de toutes les questions",
            Description = "Récupère les informations détaillées de toutes les questions enregistrées"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des questions trouvée",
            typeof(IEnumerable<QuestionQuizDTO>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune question trouvée")]
        public async Task<ActionResult<IEnumerable<QuestionQuizDTO>>> GetQuestions()
        {
            var questions = await _context
                .QuestionsQuiz.Select(q => new QuestionQuizDTO
                {
                    Id = q.Id,
                    Question = q.Question,
                    Explication = q.Explication,
                })
                .ToListAsync();

            if (!questions.Any())
            {
                return NotFound();
            }

            return Ok(questions);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Liste d'une question unique",
            Description = "Récupère les informations détaillées d'une question par son identifiant"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Question trouvée", typeof(QuestionQuizDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Question introuvable")]
        public async Task<ActionResult<QuestionQuizDTO>> GetQuestion(int id)
        {
            var question = await _context.QuestionsQuiz.SingleOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(
                new QuestionQuizDTO
                {
                    Id = question.Id,
                    Question = question.Question,
                    Explication = question.Explication,
                }
            );
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Ajouter une question",
            Description = "Ajoute une nouvelle question à la base de données"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Question ajoutée avec succès",
            typeof(QuestionQuizDTO)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Impossible d'ajouter la question")]
        public async Task<ActionResult<QuestionQuizDTO>> PostQuestion(
            [FromBody] QuestionQuizDTO questionDTO
        )
        {
            if (questionDTO == null)
            {
                return NotFound();
            }

            var question = new QuestionQuiz
            {
                Question = questionDTO.Question,
                Explication = questionDTO.Explication,
            };

            try
            {
                _context.QuestionsQuiz.Add(question);
                await _context.SaveChangesAsync();

                return Ok(
                    new QuestionQuizDTO
                    {
                        Id = question.Id,
                        Question = question.Question,
                        Explication = question.Explication,
                    }
                );
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour une question",
            Description = "Met à jour les informations d'une question existante en fonction de son identifiant"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Question mise à jour avec succès",
            typeof(QuestionQuizDTO)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Question introuvable ou non mise à jour")]
        public async Task<IActionResult> PutQuestion(int id, [FromBody] QuestionQuizDTO questionDTO)
        {
            if (id != questionDTO.Id)
            {
                return NotFound();
            }

            var questionExistante = await _context.QuestionsQuiz.FindAsync(id);
            if (questionExistante == null)
            {
                return NotFound();
            }

            questionExistante.Question = questionDTO.Question;
            questionExistante.Explication = questionDTO.Explication;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.QuestionsQuiz.Any(q => q.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(questionDTO);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer une question",
            Description = "Supprime une question existante en fonction de son identifiant"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Question supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Question introuvable")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            var question = await _context.QuestionsQuiz.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            _context.QuestionsQuiz.Remove(question);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Question supprimée avec succès" });
        }
    }
}
