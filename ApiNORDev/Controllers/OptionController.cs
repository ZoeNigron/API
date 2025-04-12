// Le contrôleur OptionController gère les opérations CRUD pour les options, telles que la récupération, la création, la mise à jour et la suppression d'options

using ApiNORDev.Model;
using ApiNORDev.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [ApiController]
    [Route("api/options")]
    public class OptionController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public OptionController(ApiNORDevContext context)
        {
            _context = context;
        }

        // pour récupérer toutes les options avec leur question associée
        [HttpGet]
        [SwaggerOperation(Summary = "Liste de toutes les options", Description = "Récupère toutes les options avec leur question associée")]
        [SwaggerResponse(StatusCodes.Status200OK, "Liste des options trouvée", typeof(IEnumerable<OptionDTO>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune option trouvée")]
        public async Task<ActionResult<IEnumerable<OptionDTO>>> GetOptions()
        {
            var options = await _context.Options
                .Include(o => o.QuestionQuiz)
                .Select(o => new OptionDTO
                {
                    Id = o.Id,
                    Texte = o.Texte,
                    EstCorrecte = o.EstCorrecte,
                    QuestionQuizId = o.QuestionQuizId
                }).ToListAsync();

            if (!options.Any())
            {
                return NotFound();
            }

            return Ok(options);
        }

        // pour récupérer une option spécifique par son id, avec la question associée
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupérer une option par ID", Description = "Renvoie une option spécifique avec sa question associée")]
        [SwaggerResponse(StatusCodes.Status200OK, "Option trouvée", typeof(OptionDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Option introuvable")]
        public async Task<ActionResult<OptionDTO>> GetOption(int id)
        {
            var option = await _context.Options
                .Include(o => o.QuestionQuiz)
                .SingleOrDefaultAsync(o => o.Id == id);

            if (option == null)
            {
                return NotFound();
            }

            return Ok(new OptionDTO
            {
                Id = option.Id,
                Texte = option.Texte,
                EstCorrecte = option.EstCorrecte,
                QuestionQuizId = option.QuestionQuizId
            });
        }

        // pour ajouter une nouvelle option associée à une question
        [HttpPost]
        [SwaggerOperation(Summary = "Ajouter une option", Description = "Ajoute une nouvelle option liée à une question quiz")]
        [SwaggerResponse(StatusCodes.Status201Created, "Option ajoutée avec succès", typeof(OptionDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Données invalides")]
        public async Task<ActionResult<OptionDTO>> PostOption([FromBody] OptionDTO optionDTO)
        {
            if (optionDTO == null || optionDTO.QuestionQuizId <= 0)
            {
                return BadRequest("L'option ou l'ID de la question est invalide.");
            }

            // pour vérifier si la question associée existe
            var questionExiste = await _context.QuestionsQuiz.AnyAsync(q => q.Id == optionDTO.QuestionQuizId);
            if (!questionExiste)
            {
                return NotFound($"La question avec l'ID {optionDTO.QuestionQuizId} n'existe pas.");
            }

            // pour créer l'option
            var option = new Option
            {
                Texte = optionDTO.Texte,
                EstCorrecte = optionDTO.EstCorrecte,
                QuestionQuizId = optionDTO.QuestionQuizId
            };

            try
            {
                _context.Options.Add(option);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetOption), new { id = option.Id }, new OptionDTO
                {
                    Id = option.Id,
                    Texte = option.Texte,
                    EstCorrecte = option.EstCorrecte,
                    QuestionQuizId = option.QuestionQuizId
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Une erreur est survenue : {ex.Message}");
            }
        }

        // pour mettre à jour une option existante
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Mettre à jour une option", Description = "Modifie une option existante")]
        [SwaggerResponse(StatusCodes.Status200OK, "Option mise à jour avec succès", typeof(OptionDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Option introuvable")]
        public async Task<IActionResult> PutOption(int id, [FromBody] OptionDTO optionDTO)
        {
            if (id != optionDTO.Id)
            {
                return BadRequest("L'ID de l'option ne correspond pas.");
            }

            var optionExistante = await _context.Options.FindAsync(id);
            if (optionExistante == null)
            {
                return NotFound($"L'option avec l'ID {id} n'existe pas.");
            }

            // vérification si la question associée existe bien
            var questionExiste = await _context.QuestionsQuiz.AnyAsync(q => q.Id == optionDTO.QuestionQuizId);
            if (!questionExiste)
            {
                return NotFound($"La question avec l'ID {optionDTO.QuestionQuizId} n'existe pas.");
            }

            // mise à jour des propriétés de l'option
            optionExistante.Texte = optionDTO.Texte;
            optionExistante.EstCorrecte = optionDTO.EstCorrecte;
            optionExistante.QuestionQuizId = optionDTO.QuestionQuizId;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(optionDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Une erreur est survenue lors de la mise à jour.");
            }
        }

        // pour supprimer une option
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer une option", Description = "Supprime une option existante")]
        [SwaggerResponse(StatusCodes.Status200OK, "Option supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Option introuvable")]
        public async Task<ActionResult> DeleteOption(int id)
        {
            var option = await _context.Options.FindAsync(id);

            if (option == null)
            {
                return NotFound($"L'option avec l'ID {id} n'existe pas.");
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Option avec l'ID {id} supprimée avec succès" });
        }
    }
}