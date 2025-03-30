using System.Collections.Generic;
using System.Linq;
using ApiNORDev.Data;
using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [Route("api/exercices")]
    [ApiController]
    public class ExercicesController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public ExercicesController(ApiNORDevContext context)
        {
            _context = context;
        }

        // GET: api/exercices
        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste de tous les exercices",
            Description = "Récupère tous les exercices disponibles"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des exercices trouvée",
            typeof(IEnumerable<Exercice>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucun exercice trouvé")]
        public ActionResult<IEnumerable<Exercice>> GetExercices()
        {
            var exercices = _context.Exercices.ToList();
            if (!exercices.Any())
            {
                return NotFound("Aucun exercice trouvé.");
            }

            return Ok(exercices);
        }

        // GET: api/exercices/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer un exercice par ID",
            Description = "Récupère un exercice spécifique à partir de son ID"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Exercice trouvé", typeof(Exercice))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Exercice introuvable")]
        public ActionResult<Exercice> GetExercice(int id)
        {
            var exercice = _context.Exercices.Find(id);

            if (exercice == null)
            {
                return NotFound($"L'exercice avec l'ID {id} n'existe pas.");
            }

            return Ok(exercice);
        }

        // POST: api/exercices
        [HttpPost]
        [SwaggerOperation(
            Summary = "Créer un nouvel exercice",
            Description = "Ajoute un nouvel exercice"
        )]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            "Exercice créé avec succès",
            typeof(Exercice)
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Les données de l'exercice sont invalides"
        )]
        public ActionResult<Exercice> PostExercice([FromBody] Exercice exercice)
        {
            if (exercice == null)
            {
                return BadRequest("Les données de l'exercice sont invalides.");
            }

            _context.Exercices.Add(exercice);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetExercice), new { id = exercice.Id }, exercice);
        }

        // PUT: api/exercices/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour un exercice",
            Description = "Met à jour un exercice existant"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Exercice mis à jour avec succès")]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Les données de l'exercice sont invalides ou l'ID ne correspond pas"
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Exercice introuvable")]
        public IActionResult PutExercice(int id, [FromBody] Exercice exercice)
        {
            if (id != exercice.Id)
            {
                return BadRequest("L'ID de l'exercice ne correspond pas.");
            }

            if (!_context.Exercices.Any(e => e.Id == id))
            {
                return NotFound($"L'exercice avec l'ID {id} n'existe pas.");
            }

            _context.Exercices.Update(exercice);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/exercices/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer un exercice",
            Description = "Supprime un exercice existant à partir de son ID"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Exercice supprimé avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Exercice introuvable")]
        public IActionResult DeleteExercice(int id)
        {
            var exercice = _context.Exercices.Find(id);

            if (exercice == null)
            {
                return NotFound($"L'exercice avec l'ID {id} n'existe pas.");
            }

            _context.Exercices.Remove(exercice);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
