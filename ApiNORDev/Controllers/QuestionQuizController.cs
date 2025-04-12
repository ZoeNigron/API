// Le contrôleur QuestionQuizController gère les opérations CRUD pour les questions des quiz, telles que la récupération, la création, la mise à jour et la suppression de questions

using ApiNORDev.Dto;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BoutiqueWeb.Controllers
{
    [ApiController]
    [Route("api/questionsquiz")]
    public class QuestionQuizController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public QuestionQuizController(ApiNORDevContext context)
        {
            _context = context;
        }

        // pour récupérer toutes les questions de quiz
        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste de toutes les questions quiz",
            Description = "Récupère toutes les questions quiz"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des questions quiz trouvée",
            typeof(IEnumerable<QuestionQuizDTO>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune question quiz trouvée")]
        public async Task<ActionResult<IEnumerable<QuestionQuizDTO>>> GetQuestionsQuiz()
        {
            var questionQuiz = await _context
                .QuestionsQuiz.Select(c => new QuestionQuizDTO(c))
                .ToListAsync();

            if (!questionQuiz.Any())
            {
                return NotFound();
            }

            return Ok(questionQuiz);
        }

        // pour récupérer une question quiz spécifique par son id
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer une question quiz par ID",
            Description = "Renvoie une question quiz spécifique"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Question quiz trouvée", typeof(QuestionQuizDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Question quiz introuvable")]
        public async Task<ActionResult<QuestionQuizDTO>> GetQuestionQuiz(
            [
                FromRoute,
                SwaggerParameter(Description = "Identifiant de la question quiz à récupérer")
            ]
                int id
        )
        {
            var questionQuiz = await _context.QuestionsQuiz.SingleOrDefaultAsync(c => c.Id == id);

            if (questionQuiz == null)
            {
                return NotFound();
            }

            return Ok(new QuestionQuizDTO(questionQuiz));
        }

        // pour ajouter une nouvelle question quiz
        [HttpPost]
        [SwaggerOperation(
            Summary = "Ajouter une nouvelle question quiz",
            Description = "Ajoute une nouvelle question quiz"
        )]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            "Question quiz ajoutée avec succès",
            typeof(QuestionQuizDTO)
        )]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Données invalides")]
        public async Task<ActionResult<QuestionQuizDTO>> PostQuestionQuiz(
            [FromBody, SwaggerParameter(Description = "Données de la question quiz à créer")]
                QuestionQuizDTO questionQuizDTO
        )
        {
            if (questionQuizDTO == null)
            {
                return NotFound();
            }

            QuestionQuiz questionQuiz = new QuestionQuiz(questionQuizDTO);

            try
            {
                _context.QuestionsQuiz.Add(questionQuiz);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetQuestionQuiz),
                    new { id = questionQuiz.Id },
                    new QuestionQuizDTO(questionQuiz)
                );
            }
            catch
            {
                return BadRequest("Une erreur est survenue lors de l'ajout de la question quiz.");
            }
        }

        // pour mettre à jour une question quiz existante
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour une question quiz",
            Description = "Modifie une question quiz existante"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Question quiz mise à jour avec succès",
            typeof(QuestionQuizDTO)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Question quiz introuvable")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Données invalides")]
        public async Task<IActionResult> PutQuestionQuiz(
            [
                FromRoute,
                SwaggerParameter(Description = "Identifiant de la question quiz à modifier")
            ]
                int id,
            [FromBody, SwaggerParameter(Description = "Données mises à jour de la question quiz")]
                QuestionQuizDTO questionQuizDTO
        )
        {
            if (id != questionQuizDTO.Id)
            {
                return BadRequest("L'ID de la question quiz ne correspond pas.");
            }

            QuestionQuiz questionQuiz = new QuestionQuiz(questionQuizDTO);

            _context.Entry(questionQuiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.QuestionsQuiz.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(questionQuizDTO);
        }

        // pour supprimer une question quiz
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer une question quiz",
            Description = "Supprime une question quiz existante"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Question quiz supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Question quiz introuvable")]
        public async Task<ActionResult> DeleteQuestionQuiz(
            [
                FromRoute,
                SwaggerParameter(Description = "Identifiant de la question quiz à supprimer")
            ]
                int id
        )
        {
            var questionQuiz = await _context.QuestionsQuiz.FindAsync(id);

            if (questionQuiz == null)
            {
                return NotFound();
            }

            _context.QuestionsQuiz.Remove(questionQuiz);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Question quiz supprimée avec succès" });
        }
    }
}