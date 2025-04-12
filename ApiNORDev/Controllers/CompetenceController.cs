// Le contrôleur CompetenceController gère les opérations CRUD pour les compétences, telles que la récupération, la création, la mise à jour et la suppression des compétences

using ApiNORDev.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiNORDev.Controllers
{
    [Route("api/competences")]
    [ApiController]
    public class CompetencesController : ControllerBase
    {
        private readonly ApiNORDevContext _context;

        public CompetencesController(ApiNORDevContext context)
        {
            _context = context;
        }

        // récupérer toutes les compétences
        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste de toutes les compétences",
            Description = "Récupère toutes les compétences disponibles"
        )]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Liste des compétences trouvée",
            typeof(IEnumerable<Competence>)
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Aucune compétence trouvée")]
        public ActionResult<IEnumerable<Competence>> GetCompetences()
        {
            var competences = _context.Competences.ToList();
            if (!competences.Any())
            {
                return NotFound("Aucune compétence trouvée.");
            }

            return Ok(competences);
        }

        // récupérer une compétence par son id
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupérer une compétence par ID",
            Description = "Récupère une compétence spécifique à partir de son ID"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Compétence trouvée", typeof(Competence))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Compétence introuvable")]
        public ActionResult<Competence> GetCompetence(int id)
        {
            var competence = _context.Competences.Find(id);

            if (competence == null)
            {
                return NotFound($"La compétence avec l'ID {id} n'existe pas.");
            }

            return Ok(competence);
        }

        // créer une nouvelle compétence
        [HttpPost]
        [SwaggerOperation(
            Summary = "Créer une nouvelle compétence",
            Description = "Ajoute une nouvelle compétence"
        )]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            "Compétence créée avec succès",
            typeof(Competence)
        )]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Les données de la compétence sont invalides"
        )]
        public ActionResult<Competence> PostCompetence([FromBody] Competence competence)
        {
            if (competence == null)
            {
                return BadRequest("Les données de la compétence sont invalides.");
            }

            _context.Competences.Add(competence);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCompetence), new { id = competence.Id }, competence);
        }

        // mettre à jour une compétence existante
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Mettre à jour une compétence",
            Description = "Met à jour une compétence existante"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Compétence mise à jour avec succès")]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            "Les données de la compétence sont invalides ou l'ID ne correspond pas"
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Compétence introuvable")]
        public IActionResult PutCompetence(int id, [FromBody] Competence competence)
        {
            if (id != competence.Id)
            {
                return BadRequest("L'ID de la compétence ne correspond pas.");
            }

            if (!_context.Competences.Any(c => c.Id == id))
            {
                return NotFound($"La compétence avec l'ID {id} n'existe pas.");
            }

            _context.Competences.Update(competence);
            _context.SaveChanges();

            return NoContent();
        }

        // supprimer une compétence par son id
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprimer une compétence",
            Description = "Supprime une compétence existante à partir de son ID"
        )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Compétence supprimée avec succès")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Compétence introuvable")]
        public IActionResult DeleteCompetence(int id)
        {
            var competence = _context.Competences.Find(id);

            if (competence == null)
            {
                return NotFound($"La compétence avec l'ID {id} n'existe pas.");
            }

            _context.Competences.Remove(competence);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
