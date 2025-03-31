using ApiNORDev.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}

/*  [HttpGet]
  public IActionResult GetAllQuizzes()
  {
      // Récupérer tous les quiz
      var quizzes = _context.Quizzes.ToList();

      // Vérifier si des quiz ont été trouvés
      if (!quizzes.Any())
      {
          return NotFound("Aucun quiz trouvé.");
      }

      // Récupérer les questions associées à chaque quiz
      var questions = _context
          .QuestionsQuiz.Where(q => quizzes.Select(quiz => quiz.Id).Contains(q.QuizId)) // Filtrer par les quiz associés
          .ToList();

      // Récupérer les options associées aux questions
      var options = _context
          .Options.Where(o => questions.Select(q => q.Id).Contains(o.QuestionQuizId)) // Filtrer par les questions associées
          .ToList();

      // Créer un DTO pour chaque quiz avec ses questions et options
      var quizDTOs = quizzes
          .Select(quiz => new QuizDTO
          {
              Id = quiz.Id,
              Titre = quiz.Titre,
              QuestionsQuiz = questions
                  .Where(q => q.QuizId == quiz.Id) // Filtrer les questions par leur QuizId
                  .Select(q => new QuestionQuizDTO(q)
                  {
                      // Ajouter les options pour chaque question
                      Options = options
                          .Where(o => o.QuestionQuizId == q.Id)
                          .Select(o => new OptionDTO(o))
                          .ToList(),
                  })
                  .ToList(),
          })
          .ToList();

      return Ok(quizDTOs);
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
