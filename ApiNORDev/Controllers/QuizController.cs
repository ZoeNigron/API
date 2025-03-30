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

        [HttpGet("{id}")]
        public IActionResult GetQuiz(int id)
        {
            // Récupérer le quiz par son ID sans utiliser 'Include' sur la propriété 'QuestionQuizIds'
            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == id); // Ne pas inclure QuestionQuizIds ici, seulement récupérer le quiz

            if (quiz == null)
            {
                return NotFound();
            }

            // Récupérer les questions associées à ce quiz en utilisant les IDs de 'QuestionQuizIds'
            var questions = _context
                .QuestionsQuiz.Where(q => quiz.QuestionQuizIds.Contains(q.Id)) // Filtrer les questions par leurs IDs
                .ToList();

            // Vérifier si des questions ont été trouvées
            if (questions.Count == 0)
            {
                return BadRequest("Le quiz ne contient pas de questions.");
            }

            // Récupérer les options associées à chaque question
            var options = _context
                .Options.Where(o => questions.Select(q => q.Id).Contains(o.QuestionQuizId))
                .ToList();

            // Créer un DTO Quiz avec les questions et leurs options
            var quizDTO = new QuizDTO
            {
                Id = quiz.Id,
                Titre = quiz.Titre,
                QuestionsQuiz = questions
                    .Select(q => new QuestionQuizDTO(q)
                    {
                        // Ajouter les options pour chaque question
                        Options = options
                            .Where(o => o.QuestionQuizId == q.Id)
                            .Select(o => new OptionDTO(o))
                            .ToList(),
                    })
                    .ToList(),
            };

            return Ok(quizDTO);
        }

        /*    [HttpGet]
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
    
           [HttpGet("{id}")]
            [SwaggerOperation(
                Summary = "Récupérer un quiz par ID",
                Description = "Récupère un quiz spécifique avec toutes ses questions et options."
            )]
            [SwaggerResponse(StatusCodes.Status200OK, "Quiz trouvé", typeof(QuizDTO))]
            [SwaggerResponse(StatusCodes.Status404NotFound, "Quiz introuvable")]
            public async Task<ActionResult<QuizDTO>> GetQuizById(int id)
            {
                var quiz = await _context
                    .Quizzes.Include(q => q.QuestionsQuiz)
                    .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == id);
    
                if (quiz == null)
                    return NotFound($"Le quiz avec l'ID {id} est introuvable.");
    
                return Ok(new QuizDTO(quiz));
            }
    
            [HttpPost]
            [SwaggerOperation(
                Summary = "Créer un nouveau quiz",
                Description = "Ajoute un quiz avec ses questions et options associées."
            )]
            [SwaggerResponse(StatusCodes.Status201Created, "Quiz créé avec succès", typeof(QuizDTO))]
            [SwaggerResponse(StatusCodes.Status400BadRequest, "Données invalides")]
            public async Task<ActionResult<QuizDTO>> CreateQuiz([FromBody] QuizDTO quizDTO)
            {
                if (quizDTO == null || string.IsNullOrWhiteSpace(quizDTO.Titre))
                    return BadRequest("Les informations du quiz sont invalides.");
    
                var quiz = new Quiz
                {
                    Titre = quizDTO.Titre,
                    QuestionsQuiz = quizDTO
                        .QuestionsQuiz?.Select(q => new QuestionQuiz
                        {
                            Question = q.Question,
                            Explication = q.Explication,
                            Options = new List<Option>(),
                        })
                        .ToList(),
                };
    
                _context.Quizzes.Add(quiz);
                await _context.SaveChangesAsync();
    
                foreach (var questionDTO in quizDTO.QuestionsQuiz)
                {
                    var questionEntity = quiz.QuestionsQuiz.FirstOrDefault(q =>
                        q.Question == questionDTO.Question
                    );
                    if (questionEntity != null)
                    {
                        questionEntity.Options = questionDTO
                            .Options.Select(o => new Option
                            {
                                Texte = o.Texte,
                                EstCorrecte = o.EstCorrecte,
                                QuestionQuizId = questionEntity.Id,
                            })
                            .ToList();
                    }
                }
    
                await _context.SaveChangesAsync();
    
                return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, new QuizDTO(quiz));
            }
    
            // 🔹 4. Modifier un quiz existant
            [HttpPut("{id}")]
            [SwaggerOperation(
                Summary = "Mettre à jour un quiz",
                Description = "Modifie un quiz existant avec ses questions et options."
            )]
            [SwaggerResponse(StatusCodes.Status200OK, "Quiz mis à jour avec succès", typeof(QuizDTO))]
            [SwaggerResponse(StatusCodes.Status404NotFound, "Quiz introuvable")]
            public async Task<IActionResult> UpdateQuiz(int id, [FromBody] QuizDTO quizDTO)
            {
                if (id != quizDTO.Id)
                    return BadRequest("L'ID du quiz ne correspond pas.");
    
                var existingQuiz = await _context
                    .Quizzes.Include(q => q.QuestionsQuiz)
                    .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == id);
    
                if (existingQuiz == null)
                    return NotFound($"Le quiz avec l'ID {id} est introuvable.");
    
                existingQuiz.Titre = quizDTO.Titre;
    
                _context.QuestionsQuiz.RemoveRange(existingQuiz.QuestionsQuiz);
    
                existingQuiz.QuestionsQuiz = quizDTO
                    .QuestionsQuiz?.Select(q => new QuestionQuiz
                    {
                        Question = q.Question,
                        Explication = q.Explication,
                        Options = q
                            .Options.Select(o => new Option
                            {
                                Texte = o.Texte,
                                EstCorrecte = o.EstCorrecte,
                            })
                            .ToList(),
                    })
                    .ToList();
    
                await _context.SaveChangesAsync();
                return Ok(new QuizDTO(existingQuiz));
            }
    
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
            }*/
    }
}
